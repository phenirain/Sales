using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Repositories.Abstracts;
using Sales.Services.Dtos.CreateUpdate;

namespace Sales.Repositories;

public class BuyerRepository: AbstractRepository<BuyerDbModel, Buyer>
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
        var buyers = new Buyer[limit];
        for (var i = 0; i < buyerDbModels.Count; i++)
        {
            buyers[i] = Mapper.Map(buyerDbModels[i], new Buyer());
            buyers[i].SalesIds = buyerDbModels[i].Sales.Select(s => s.Id).ToList();
        }
        return buyers;
    }

    public override async Task<Buyer?> GetById(long id)
    {
        var buyerDbModel = await Context.Buyers
           .AsNoTracking()
           .Where(b => b.Id == id)
           .Include(b => b.Sales)
           .FirstOrDefaultAsync();
        if (buyerDbModel == null) return null;
        var buyer = Mapper.Map<Buyer>(buyerDbModel);
        buyer.SalesIds = buyerDbModel.Sales.Select(s => s.Id).ToList();
        return buyer;
    }
}