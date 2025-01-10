using AutoMapper;
using Sales.Contexts;
using Sales.Entities.DomainModels;
using Sales.Repositories.Abstracts;
using Sales.Repositories.Interfaces;

namespace Sales.Repositories;

public class Repository<DbModel, Model>: AbstractRepository<DbModel, Model> where DbModel : class, IGetId where Model : IGetId
{
    public Repository(Context context, IMapper mapper) : base(context, mapper)
    {
    }
}