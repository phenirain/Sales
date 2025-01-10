using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Middlewares;
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

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.Run();
