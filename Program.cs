using Microsoft.EntityFrameworkCore;
using Sales.Contexts;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load(".env");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = Environment.GetEnvironmentVariable("DbConnectionString");

if (connectionString is null)
{
    throw new ArgumentNullException("DbConnectionString is not set");
}

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
