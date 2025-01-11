using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;

namespace Sales.Repositories.Abstracts;

public abstract class AbstractRepository<Model, DbModel> : IRepository<Model, DbModel> where Model: BaseModel where DbModel : BaseDbModel
{
    protected readonly Context Context;
    protected readonly IMapper Mapper;

    protected AbstractRepository(Context context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }
    
    
    public virtual async Task<IEnumerable<Model>> GetAll(int limit, int offset)
    {
        return await Context.Set<DbModel>()
            .AsNoTracking()
            .Skip(offset)
            .Take(limit)
            .ProjectTo<Model>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public virtual async Task<Model?> GetById(long id)
    {
        return await Context.Set<DbModel>().ProjectTo<Model>(Mapper.ConfigurationProvider).Where(m => m.Id == id).FirstOrDefaultAsync();
    }

    public async Task<DbModel> Create(Model model)
    {
        var dbModel = Mapper.Map<DbModel>(model);
        await Context.Set<DbModel>().AddAsync(dbModel);
        return dbModel;
    }

    public async Task Update(Model model)
    {
        var dbModel = await Context.Set<DbModel>().Where(m => m.Id == model.Id).FirstOrDefaultAsync();
        if (dbModel == null)
        {
            throw new NotFoundException(typeof(Model), model.Id);
        }

        Mapper.Map(model, dbModel);
        Context.Set<DbModel>().Update(dbModel);
    }

    public async Task Delete(Model model)
    {
        var dbModel = Context.Set<DbModel>().Where(m => m.Id == model.Id).FirstOrDefault();
        if (dbModel == null)
        {
            throw new NotFoundException(typeof(Model), model.Id);
        }
        Context.Set<DbModel>().Remove(dbModel);
    }
}