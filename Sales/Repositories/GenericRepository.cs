using AutoMapper;
using Sales.Contexts;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Repositories.Abstracts;
using Sales.Repositories.Interfaces;

namespace Sales.Repositories;

public class GenericRepository<Model, DbModel>: AbstractRepository<Model, DbModel> where DbModel : BaseDbModel where Model : BaseModel
{
    public GenericRepository(Context context, IMapper mapper) : base(context, mapper)
    {
    }
}