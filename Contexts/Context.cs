using Microsoft.EntityFrameworkCore;
using Sales.Entities.DbModels;

namespace Sales.Contexts;

public class Context: DbContext
{

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    public DbSet<ProductDbModel> Products { get; set; }
    public DbSet<ProvidedProductDbModel> ProvidedProducts { get; set; }
    public DbSet<SalesPointDbModel> SalesPoints { get; set; }
    public DbSet<SaleDataDbModel> SalesData { get; set; }
    public DbSet<SaleDbModel> Sales { get; set; }
    public DbSet<BuyerDbModel> Buyers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<SalesPointDbModel>(entity =>
        {
            entity.OwnsMany(e => e.ProvidedProducts, p =>
            {
                p.WithOwner();
            });
        });
        
        mb.Entity<SaleDbModel>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.SalesPointDbModel)
                .WithMany(sp => sp.Sales)
                .HasForeignKey(e => e.SalesPointId);
            
            entity.HasOne(e => e.BuyerDbModel)
                .WithMany(b => b.SalesIds)
                .HasForeignKey(e => e.BuyerId);

            entity.OwnsMany(e => e.SaleData, sd =>
            {
                sd.WithOwner();
            });
        });
    }
}