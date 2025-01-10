using AutoMapper;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.Mapping;
using Sales.Repositories;
using Sales.Repositories.Interfaces;
using Sales.Services.Mapping;
using Sales.Support;

namespace Sales.Tests.Support;

public static class DatabaseTestHelper
{
    private static Context _context;
    private static Faker _faker;
    
    public static async Task<IUnitOfWork> CreateUnitOfWork()
    {
        _context = CreateTestContext();
        var mapper = CreateMapper();
        _faker = new Faker("en");
        return new UnitOfWork(_context, mapper);
    }

    public static async Task CreateFakeBuyers(int number = 20)
    {
        if (_faker == null)
            throw new ArgumentNullException("Faker is not set");
        var buyers = new Faker<BuyerDbModel>()
            .RuleFor(b => b.Name, f => f.Name.FindName())
            .Generate(number);
        _context.Buyers.AddRange(buyers);
        await _context.SaveChangesAsync();
    }

    public static async Task CreateFakeProducts(int number = 20)
    {
        if (_faker == null)
            throw new ArgumentNullException("Faker is not set");
        var products = new Faker<ProductDbModel>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
            .Generate(number);
        _context.Products.AddRange(products);
        await _context.SaveChangesAsync();
    }

    public static async Task CreateFakeSalesPoints(int number = 10)
    {
        if (_faker == null)
            throw new ArgumentNullException("Faker is not set");
        var salesPoints = new Faker<SalesPointDbModel>()
            .RuleFor(sp => sp.Name, f => f.Company.CompanyName())
            .RuleFor(sp => sp.ProvidedProducts, f => new Faker<ProvidedProductDbModel>()
                .RuleFor(pp => pp.ProductId, f => f.Random.Int(1, 20))
                .RuleFor(pp => pp.ProductQuantity, f => f.Random.Int(1, 5))
                .Generate(5)
            )
            .Generate(number);
        _context.SalesPoints.AddRange(salesPoints);
        await _context.SaveChangesAsync();
    }

    public static async Task CreateFakeSales(int number = 10, long buyerId = 1)
    {
        if (_faker == null)
            throw new ArgumentNullException("Faker is not set");
        if (!_context.Products.Any())
            await CreateFakeProducts();
        if (!_context.Buyers.Any())
            await CreateFakeBuyers();
        if (!_context.SalesPoints.Any())
            await CreateFakeSalesPoints();

        var sales = new Faker<SaleDbModel>()
            .RuleFor(s => s.Date, f => DateOnly.FromDateTime(f.Date.Past()))
            .RuleFor(s => s.Time, f => TimeOnly.FromDateTime(f.Date.Past()))
            .RuleFor(s => s.SalesPointId, f => f.Random.Number(1, 10))
            .RuleFor(s => s.BuyerId, f => f.Random.Number(1, 20))
            .Generate(number);

        var products = await _context.Products.ToListAsync();
        foreach (var sale in sales)
        {
            var saleData = new List<SaleDataDbModel>();
            for (int i = 0; i < 5; i++)
            {
                var productId = new Faker().Random.Number(1, 20);
                var productQuantity = new Faker().Random.Number(1, 5);
                var dbModel = new SaleDataDbModel()
                {
                    ProductId = productId,
                    ProductQuantity = productQuantity,
                    ProductIdAmount = products.First(p => p.Id == productId).Price * productQuantity
                };
                saleData.Add(dbModel);
            }

            sale.SaleData = saleData;
        }
        
        sales.Add(
            new ()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                SalesPointId = 1,
                BuyerId = buyerId,
                SaleData = new List<SaleDataDbModel>(),
            }
        );
        
        _context.Sales.AddRange(sales);
        await _context.SaveChangesAsync();
    }
    
    
    private static Context CreateTestContext()
    {
        DotNetEnv.Env.Load(".env");
        // var connectionString = Environment.GetEnvironmentVariable("DbConnectionString");
        var connectionString = "Host=localhost;Port=5432;Database=TestSales;Username=phenirain;Password=ru13aaogh06(";
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