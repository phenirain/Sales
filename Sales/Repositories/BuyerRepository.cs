using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Repositories.Abstracts;
using Sales.Services.Dtos.CreateUpdate;

namespace Sales.Repositories;

public class BuyerRepository: AbstractRepository<Buyer, BuyerDbModel>
{
    public BuyerRepository(Context context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<IEnumerable<Buyer>> GetAll(int limit, int offset)
    {
        var buyerDbModels = await Context.Buyers
            .AsNoTracking()
            .Skip(offset)
            .Take(limit)
            .Include(b => b.Sales)
            .ToListAsync();
        return Mapper.Map<List<Buyer>>(buyerDbModels);
    }

    public override async Task<Buyer?> GetById(long id)
    {
        var buyerDbModel = await Context.Buyers
           .AsNoTracking()
           .Where(b => b.Id == id)
           .Include(b => b.Sales)
           .FirstOrDefaultAsync();
        return Mapper.Map<Buyer>(buyerDbModel);
    }
}