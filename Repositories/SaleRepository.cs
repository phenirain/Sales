using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Repositories.Abstracts;
using Sales.Repositories.Interfaces;

namespace Sales.Repositories;

public class SaleRepository: AbstractRepository<SaleDbModel, Sale>
{

    public SaleRepository(Context context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<IEnumerable<Sale>> GetAll(int limit, int offset)
    {
        return await Context.Sales
            .Skip(offset)
            .Take(limit)
            .Include(s => s.SaleData)
            .ProjectTo<Sale>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public override async Task<Sale?> GetById(long id)
    {
        return await Context.Sales
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Include(s => s.SaleData)
            .ProjectTo<Sale>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}