using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;

namespace Sales.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly Context _context;
    private IDbContextTransaction? _currentTransaction;
    
    public IRepository<Buyer> BuyerRepository { get;}
    public IRepository<Product> ProductRepository { get; }
    public IRepository<Sale> SaleRepository { get; }
    public IRepository<SalesPoint> SalesPointRepository { get; }

    public UnitOfWork(Context context, IMapper mapper)
    {
        _context = context;
        
        BuyerRepository = new BuyerRepository(_context, mapper);
        ProductRepository = new GenericRepository<ProductDbModel, Product>(_context, mapper);
        SaleRepository = new SaleRepository(_context, mapper);
        SalesPointRepository = new SalesPointRepository(_context, mapper);
    }

    public IRepository<TModel> GetRepository<TModel>()
    {
        // В теории можно реализовать через DI Container, и в конструкторе UnitOfWork вытягивать объекты классов из DI
        // а тут просто через TModel получать из DI нужный репозиторий
        if (typeof(TModel) == typeof(Buyer))
            return (IRepository<TModel>)BuyerRepository;
        if (typeof(TModel) == typeof(Product))
            return (IRepository<TModel>)ProductRepository;
        if (typeof(TModel) == typeof(Sale))
            return (IRepository<TModel>)SaleRepository;
        if (typeof(TModel) == typeof(SalesPoint))
            return (IRepository<TModel>)SalesPointRepository;
        throw new RepositoryNotFound(typeof(TModel).Name);
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction == null) 
            _currentTransaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_currentTransaction != null)
        {
            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch (Exception e)
            {
                await _currentTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        } 
    }

    public async Task RollbackAsync()
    {
        if (_currentTransaction != null)
        {
            try
            {
                await _currentTransaction.RollbackAsync();
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
}