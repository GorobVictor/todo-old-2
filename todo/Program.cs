using System.Reflection;
using AutoMapper;
using core.AutoMapper;
using core.Interfaces;
using infrastructure;
using infrastructure.Services;
using infrastructure.Statics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,

            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,

            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.Scan(scan =>
    scan.FromAssemblyDependencies(Assembly.Load("Infrastructure"))
        .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Repository") &&
                                                  t.Namespace == "infrastructure.Repositories"))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

builder.Services.Scan(scan =>
    scan.FromAssemblyDependencies(Assembly.Load("Infrastructure"))
        .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Service") &&
                                                  t.Namespace == "infrastructure.Services"))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSingleton<IMyAuthorizationServiceSingelton, MyAuthorizationServiceSingelton>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoContext>(options => options.UseSqlServer(connection));

builder.Services.AddSingleton(x =>
    new MapperConfiguration(m =>
        m.AddProfile(new MapperProfile())
    ).CreateMapper()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();