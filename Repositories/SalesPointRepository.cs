using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Repositories.Abstracts;

namespace Sales.Repositories;

public class SalesPointRepository: AbstractRepository<SalesPointDbModel, SalesPoint>
{
    public SalesPointRepository(Context context, IMapper mapper) : base(context, mapper)
    {
    }
    
    public override async Task<IEnumerable<SalesPoint>> GetAll(int limit, int offset)
    {
        return await Context.SalesPoints
            .Skip(offset)
           .Take(limit)
            .Include(sp => sp.ProvidedProducts)
            .ProjectTo<SalesPoint>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }
    
    public override async Task<SalesPoint?> GetById(long id)
    {
        return await Context.SalesPoints
            .Where(sp => sp.Id == id)
            .ProjectTo<SalesPoint>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}