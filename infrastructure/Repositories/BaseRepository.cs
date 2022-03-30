using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using core.Entities.Base;
using core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : FullEntity
{
    protected readonly ToDoContext dbContext;
    protected readonly IMapper mapper;

    public BaseRepository(ToDoContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }


    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) =>
        this.dbContext.Set<TEntity>().AsNoTracking().Where(predicate);

    public IQueryable<T> Get<T>(Expression<Func<TEntity, bool>> predicate) =>
        this.dbContext.Set<TEntity>().AsNoTracking().Where(predicate).ProjectTo<T>(this.mapper.ConfigurationProvider);

    public IQueryable<TEntity> Get() =>
        this.dbContext.Set<TEntity>().AsNoTracking();

    public IQueryable<T> Get<T>() =>
        this.dbContext.Set<TEntity>().AsNoTracking().ProjectTo<T>(this.mapper.ConfigurationProvider);

    public async Task<TEntity?> GetByIdAsync(int id, bool track = false) =>
        await this.dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public TEntity Add(TEntity entity)
    {
        this.dbContext.Entry(entity).State = EntityState.Added;

        this.dbContext.SaveChanges();

        this.dbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }

    public List<T> Add<T>(List<T> entities)
    {
        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Added;
        }

        this.dbContext.SaveChanges();

        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Detached;
        }

        return entities;
    }

    public async Task<T> AddAsync<T>(T entity)
    {
        this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Added;

        await this.dbContext.SaveChangesAsync();

        this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Detached;

        return entity;
    }

    public async Task<List<T>> AddAsync<T>(List<T> entities)
    {
        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Added;
        }

        await this.dbContext.SaveChangesAsync();

        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Detached;
        }

        return entities;
    }

    public async Task<T> UpdateAsync<T>(T entity)
    {
        this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Modified;

        await this.dbContext.SaveChangesAsync();

        this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Detached;

        return entity;
    }

    public async Task<List<T>> UpdateAsync<T>(List<T> entities)
    {
        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Modified;
        }

        await this.dbContext.SaveChangesAsync();

        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Detached;
        }

        return entities;
    }

    public T Update<T>(T entity)
    {
        this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Modified;

        this.dbContext.SaveChanges();

        this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Detached;

        return entity;
    }

    public List<T> Update<T>(List<T> entities)
    {
        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Modified;
        }

        this.dbContext.SaveChanges();

        foreach (var entity in entities)
        {
            this.dbContext.Entry(entity ?? throw new InvalidOperationException()).State = EntityState.Detached;
        }

        return entities;
    }
}