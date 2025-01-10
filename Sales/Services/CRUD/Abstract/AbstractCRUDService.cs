using AutoMapper;
using Sales.Entities.DomainModels;
using Sales.Entities.Interfaces;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;
using Sales.Services.Dtos.Get;
using Sales.Services.Interfaces;

namespace Sales.Services.CRUD.Abstract;

public class AbstractCRUDService<CreateDto, UpdateDto, GetDto, TModel, DbModel>: ICRUDService<CreateDto, UpdateDto, GetDto>
    where TModel: ISetId
    where DbModel: IGetId
{
    protected readonly IUnitOfWork UnitOfWork;
    protected readonly IMapper Mapper;
    protected readonly ILogger<AbstractCRUDService<CreateDto, UpdateDto, GetDto, TModel, DbModel>> Logger;
    
    public AbstractCRUDService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AbstractCRUDService<CreateDto, UpdateDto, GetDto, TModel, DbModel>> logger)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
        Logger = logger;
    }
    
    
    public async Task<IEnumerable<GetDto>> GetAll(int limit, int offset)
    {
        try
        {
            return Mapper.Map<IEnumerable<GetDto>>(await UnitOfWork.GetRepository<TModel, DbModel>().GetAll(limit, offset));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred while getting all {typeof(TModel).Name}s.");
            throw;
        }
    }

    public async Task<GetDto> GetById(long id)
    {
        try
        {
            var result = await GetModelById(id);
            return Mapper.Map<GetDto>(result);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred while getting {typeof(TModel).Name} by id: {id}.");
            throw;
        }
    }

    public virtual async Task<long> Create(CreateDto createDto)
    {
        try
        {
            var model = Mapper.Map<TModel>(createDto);
            var createdModel = await UnitOfWork.GetRepository<TModel, DbModel>().Create(model);
            await UnitOfWork.SaveChangesAsync();
            return createdModel.Id;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred while creating a new {typeof(TModel).Name}.");
            throw;
        }
    }

    public virtual async Task Update(long id, UpdateDto updateDto)
    {
        try
        {
            var model = Mapper.Map<TModel>(updateDto);
            model.Id = id;
            await UnitOfWork.GetRepository<TModel, DbModel>().Update(model);
            await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred while updating {typeof(TModel).Name} by id: {id}.");
            throw;
        }
    }

    public async Task Delete(long id)
    {
        try
        {
            var model = await GetModelById(id);
            await UnitOfWork.GetRepository<TModel, DbModel>().Delete(model);
            await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"An error occurred while deleting {typeof(TModel).Name} by id: {id}.");
            throw;
        }
    }

    protected async Task<TModel> GetModelById(long id)
    {
        var result = await UnitOfWork.GetRepository<TModel, DbModel>().GetById(id);
        if (result == null)
            throw new NotFoundException(typeof(TModel), id);
        return result;
    }
}