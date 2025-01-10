using AutoMapper;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Entities.ValueObjects;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;
using Sales.Services.CRUD.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;

namespace Sales.Services.CRUD;

public class SalesPointCRUDService: AbstractCRUDService<SalesPointCreateDto, SalesPointUpdateDto, SalesPointGetDto, SalesPoint, SalesPointDbModel>
{
    public SalesPointCRUDService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SalesPointCRUDService> logger)
        : base(unitOfWork, mapper, logger)
    {
    }

    public override async Task Update(long id, SalesPointUpdateDto dto)
    {
        try
        {
        SalesPoint salesPoint = await GetModelById(id);
        var updatedSalesPoint = Mapper.Map(dto, salesPoint);

        if (dto.AddedProvidedProducts.Count > 0)
        {
            foreach (var ppDto in dto.AddedProvidedProducts)
            {
                var oldPp =
                    salesPoint.ProvidedProducts.FirstOrDefault(pp => pp.ProductId == ppDto.ProductId);
                if (oldPp == null)
                    updatedSalesPoint.ProvidedProducts.Add(Mapper.Map<ProvidedProduct>(ppDto));
                else
                {
                    oldPp.ProductQuantity += ppDto.ProductQuantity;
                }
            }
        }

        if (dto.UpdatedProvidedProducts.Count > 0)
        {
            foreach (var ppDto in dto.AddedProvidedProducts)
            {
                var oldPp = 
                    salesPoint.ProvidedProducts.FirstOrDefault(pp => pp.ProductId == ppDto.ProductId);
                if (oldPp != null)
                {
                    oldPp.ProductQuantity = ppDto.ProductQuantity;
                }
                else
                {
                    throw new NotFoundException($"ProvidedProduct with ProductId: {ppDto.ProductId} not found");
                }
            }
        }

        if (dto.RemovedProvidedProducts.Count > 0)
        {
            foreach (var ppDto in dto.RemovedProvidedProducts)
            {
                var oldPp = 
                    salesPoint.ProvidedProducts.FirstOrDefault(pp => pp.ProductId == ppDto.ProductId);
                if (oldPp!= null)
                    updatedSalesPoint.ProvidedProducts.Remove(oldPp);
                else
                {
                    throw new NotFoundException($"ProvidedProduct with ProductId: {ppDto.ProductId} not found");
                }
            }
        }
        
        await UnitOfWork.SalesPointRepository.Update(updatedSalesPoint);
        await UnitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error updating SalesPoint with id: {id}: {ex.Message}");
            throw;
        }
    }
    
    
}