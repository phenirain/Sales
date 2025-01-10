using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.Mapping;
using Sales.Middlewares;
using Sales.Repositories;
using Sales.Repositories.Interfaces;
using Sales.Services.Business;
using Sales.Services.CRUD;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;
using Sales.Services.Interfaces;
using Sales.Services.Mapping;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load(".env");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext}: {Message}{NewLine}{Exception}")
    .WriteTo.File("./Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext}: {Message}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DbConnectionString");

if (connectionString is null)
{
    throw new ArgumentNullException("DbConnectionString is not set");
}

#region dependencies

builder.Services.AddAutoMapper(typeof(EntityMapperProfile), typeof(ServiceMapperProfile));

// infrastructure
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// services

// business
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISaleService, SaleService>();

// CRUD
builder.Services.AddScoped<ICRUDService<SaleCreateDto, SaleUpdateDto, SaleGetDto>, SaleCRUDService>();
builder.Services.AddScoped<ICRUDService<SalesPointCreateDto, SalesPointUpdateDto, SalesPointGetDto>, SalesPointCRUDService>();
builder.Services.AddScoped<ICRUDService<BuyerDto, BuyerDto, BuyerGetDto>, BuyerCRUDService>();
builder.Services.AddScoped<ICRUDService<ProductDto, ProductDto, ProductGetDto>, ProductCRUDService>();

// controllers
builder.Services.AddControllers();

#endregion



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sales API v1");
});

app.UsePathBase("/api/v1/");
app.MapControllers();
app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.Run();
