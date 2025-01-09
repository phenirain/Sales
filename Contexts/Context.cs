using Microsoft.EntityFrameworkCore;
using Sales.Entities.DbModels;

namespace Sales.Contexts;

public class Context: DbContext
{

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
    public DbSet<SalesPoint> SalesPoints { get; set; }
    public DbSet<SaleData> SalesData { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Buyer> Buyers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.Entity<SalesPoint>(entity =>
        {
            entity.OwnsMany(e => e.ProvidedProducts, p =>
            {
                p.WithOwner();
            });
        });
        
        mb.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.SalesPoint)
                .WithMany(sp => sp.Sales)
                .HasForeignKey(e => e.SalesPointId);
            
            entity.HasOne(e => e.Buyer)
                .WithMany(b => b.SalesIds)
                .HasForeignKey(e => e.BuyerId);

            entity.OwnsMany(e => e.SalesData, sd =>
            {
                sd.WithOwner();
            });
        });
    }
}