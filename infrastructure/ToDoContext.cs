using System.Reflection.Metadata;
using core.Entities;
using core.Entities.Base;
using core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Task = core.Entities.Task;

namespace infrastructure;

public class ToDoContext : DbContext
{
    private IHttpContextAccessor ContextAccessor { get; set; }
    private IMyAuthorizationServiceSingelton MyAuthorizationServiceSingelton { get; set; }
    
    public DbSet<User>? Users { get; set; }
    public DbSet<Project>? Projects { get; set; }
    public DbSet<Status>? Status { get; set; }
    public DbSet<ProjectStatus>? ProjectStatus { get; set; }
    public DbSet<ProjectUser>? ProjectUsers { get; set; }
    public DbSet<Task>? Tasks { get; set; }

    public ToDoContext(
        DbContextOptions<ToDoContext> options, 
        IHttpContextAccessor context, 
        IMyAuthorizationServiceSingelton myAuthorizationServiceSingelton
        )
        : base(options)
    {
        this.ContextAccessor = context;
        this.MyAuthorizationServiceSingelton = myAuthorizationServiceSingelton;
    }
    public async Task<int> SaveChangesAsync()
    {
        this.AddTimestamps();

        return await base.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        this.AddTimestamps();

        return base.SaveChanges();
    }

    private void AddTimestamps()
    {
        int userId;
        if (ContextAccessor.HttpContext.Request.Path.Value.Contains("sign-up"))
            userId = -1;
        else
            userId = MyAuthorizationServiceSingelton.UserIdAuthenticatedOrNull ?? -1;

        var entities = this.ChangeTracker.Entries().Where(x => x.Entity is FullEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((FullEntity)entity.Entity).CreatedAt = DateTime.Now;
                ((FullEntity)entity.Entity).CreatedBy = userId;
            }

            if (entity.State == EntityState.Modified)
            {
                ((FullEntity)entity.Entity).UpdatedAt = DateTime.Now;
                ((FullEntity)entity.Entity).UpdatedBy = userId;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>().HasQueryFilter(x => !x.Deleted);
        modelBuilder.Entity<ProjectStatus>().HasQueryFilter(x => !x.Deleted);
        modelBuilder.Entity<ProjectUser>().HasQueryFilter(x => !x.Deleted);
        modelBuilder.Entity<Status>().HasQueryFilter(x => !x.Deleted);
        modelBuilder.Entity<Task>().HasQueryFilter(x => !x.Deleted);
        modelBuilder.Entity<User>().HasQueryFilter(x => !x.Deleted);
    }
}