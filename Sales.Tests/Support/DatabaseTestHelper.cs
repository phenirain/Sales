using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.Mapping;
using Sales.Repositories;
using Sales.Repositories.Interfaces;
using Sales.Services.Mapping;
using Sales.Support;

namespace Sales.Tests.Support;

public static class TestHelper
{
    private static Context _context;
    
    public static IUnitOfWork CreateUnitOfWork()
    {
        _context = CreateTestContext();
        var mapper = CreateMapper();
        return new UnitOfWork(_context, mapper);
    }
    
    private static Context CreateTestContext()
    {
        var connectionString = DotNetEnv.Env.GetString("DbConnectionString");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("DbConnectionString is not set");
        }

        var options = new DbContextOptionsBuilder<Context>()
            .UseNpgsql(connectionString)
            .Options;
        var context = new Context(options);
        
        context.Database.Migrate();
        
        return context;
    }

    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EntityMapperProfile>();
            cfg.AddProfile<ServiceMapperProfile>();
        });
        return config.CreateMapper();
    }

    public static void ResetDatabase()
    {
        _context.Database.EnsureDeleted();
    }
}