using AutoMapper;
using Sales.Entities.Interfaces;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;
using Sales.Services.Dtos.Get;
using Sales.Services.Interfaces;

namespace Sales.Services.CRUD.Abstract;

public class AbstractCRUDService<CreateDto, UpdateDto, GetDto, TModel>: ICRUDService<CreateDto, UpdateDto, GetDto>
    where TModel: ISetId
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;
    
    public AbstractCRUDService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }
    
    
    public async Task<IEnumerable<GetDto>> GetAll(int limit, int offset)
    {
        return Mapper.Map<IEnumerable<GetDto>>(await UnitOfWork.GetRepository<TModel>().GetAll(limit, offset));
    }

    public async Task<GetDto> GetById(long id)
    {
        var result = await GetModelById(id);
        return Mapper.Map<GetDto>(result);
    }

    public virtual async Task<long> Create(CreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);
        var model = Mapper.Map<TModel>(createDto);
        long id = await UnitOfWork.GetRepository<TModel>().Create(model);
        await UnitOfWork.SaveChangesAsync();
        return id;
    }

    public virtual async Task Update(long id, UpdateDto updateDto)
    {
        ArgumentNullException.ThrowIfNull(updateDto);
        var model = Mapper.Map<TModel>(updateDto);
        model.Id = id;
        await UnitOfWork.GetRepository<TModel>().Update(model);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var model = await GetModelById(id);
        UnitOfWork.GetRepository<TModel>().Delete(model);
        await UnitOfWork.SaveChangesAsync();
    }

    protected async Task<TModel> GetModelById(long id)
    {
        var result = await UnitOfWork.GetRepository<TModel>().GetById(id);
        if (result == null)
            throw new NotFoundException(typeof(TModel), id);
        return result;
    }
}