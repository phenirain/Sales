using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;

namespace Sales.Support;

public static class DatabaseInitializer
{
    public static void Seed(Context context)
    {
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new ProductDbModel { Name = "Product A", Price = 100 },
                new ProductDbModel { Name = "Product B", Price = 200 },
                new ProductDbModel { Name = "Product C", Price = 300 }
            );
            context.SaveChanges();
        }

        if (!context.SalesPoints.Any())
        {
            context.SalesPoints.AddRange(
                new SalesPointDbModel()
                {
                    Name = "SalesPoint 1",
                    ProvidedProducts = new List<ProvidedProductDbModel>
                    {
                        new () { ProductId = 1, ProductQuantity = 50 },
                        new () { ProductId = 2, ProductQuantity = 30 }
                    }
                },
                new SalesPointDbModel()
                {
                    Name = "SalesPoint 2",
                    ProvidedProducts = new List<ProvidedProductDbModel>
                    {
                        new () { ProductId = 3, ProductQuantity = 20 },
                        new () { ProductId = 1, ProductQuantity = 10 }
                    }
                }
            );
            context.SaveChanges();
        }

        if (!context.Buyers.Any())
        {
            context.Buyers.AddRange(
                new BuyerDbModel { Name = "John Doe" },
                new BuyerDbModel { Name = "Jane Smith" }
            );
            context.SaveChanges();
        }

        if (!context.Sales.Any())
        {
            context.Sales.AddRange(
                new SaleDbModel
                {
                    Date = new DateOnly(2025, 1, 10),
                    Time = new TimeOnly(10, 30),
                    SalesPointId = 1,
                    BuyerId = 1,
                    SaleData = new List<SaleDataDbModel>
                    {
                        new () { ProductId = 1, ProductQuantity = 3, ProductIdAmount = 300 }
                    },
                    TotalAmount = 300
                },
                new SaleDbModel
                {
                    Date = new DateOnly(2025, 1, 10),
                    Time = new TimeOnly(11, 0),
                    SalesPointId = 2,
                    BuyerId = null,
                    SaleData = new List<SaleDataDbModel>
                    {
                        new () { ProductId = 3, ProductQuantity = 2, ProductIdAmount = 600 }
                    },
                    TotalAmount = 600
                }
            );
            context.SaveChanges();
        }
    }
}