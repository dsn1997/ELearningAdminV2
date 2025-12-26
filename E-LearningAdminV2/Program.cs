using Backend.Infrastructure.Middleware;
using IIG.Application.DI;
using IIG.Application.Services;
using IIG.Core.DI;
using IIG.Core.Entities;
using IIG.Core.Interface;
using IIG.Core.Interface.UnitOfWork;
using IIG.Core.Repository;
using IIG.Core.Utils;
using IIG.EntityFrameworkCore.EntityFramework.Repository;
using IIG.EntityFrameworkCore.EntityFramework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

ServiceCoreExtensions.AddServiceCoreConfig(builder.Services, builder.Configuration);
ServiceApplicationExtensions.AddServiceApplicationConfig(builder.Services, builder.Configuration);
// Add services to the container.

// Add EF DbContext
AppSettings.Instance.SetConfiguration(builder.Configuration);
builder.Services.AddDbContextFactory<IIGDbContext>(opts => opts.UseSqlServer(AppSettings.Instance.Get("DbConnectionStrings:SqlServerConnection", "")));

builder.Services.AddScoped<IActiveTransactionProvider, ActiveTransactionProvider>();
builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>(); 

// repositories & services
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<CustomUnitOfWorkMiddleware>();

app.MapControllers();

app.Run();
