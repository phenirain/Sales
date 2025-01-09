using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;

namespace Sales.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Buyer> BuyerRepository { get;}
    public IRepository<Product> ProductRepository { get; }
    public IRepository<Sale> SaleRepository { get; }
    public IRepository<SalesPoint> SalesPointRepository { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}