using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;

namespace Sales.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IRepository<Buyer, BuyerDbModel> BuyerRepository { get;}
    public IRepository<Product, ProductDbModel> ProductRepository { get; }
    public IRepository<Sale, SaleDbModel> SaleRepository { get; }
    public IRepository<SalesPoint, SalesPointDbModel> SalesPointRepository { get; }

    IRepository<TModel, DbModel> GetRepository<TModel, DbModel>(); 
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}