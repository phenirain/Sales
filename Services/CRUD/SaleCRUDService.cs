using AutoMapper;
using Sales.Entities.DomainModels;
using Sales.Entities.ValueObjects;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;
using Sales.Services.CRUD.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;
using Sales.Services.Dtos.ValueObjectDtos;

namespace Sales.Services.CRUD;

public class SaleCRUDService: AbstractCRUDService<SaleCreateDto, SaleUpdateDto, SaleGetDto, Sale>
{
    public SaleCRUDService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    public override async Task<long> Create(SaleCreateDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        var sale = Mapper.Map<Sale>(dto);
        foreach (var saleDataDto in dto.SaleData)
        {
            var saleData = await GetSaleData(saleDataDto);
            sale.AddSaleData(saleData);
        }
        long id = await UnitOfWork.SaleRepository.Create(sale);
        await UnitOfWork.SaveChangesAsync();
        return id;
    }

    public override async Task Update(long id, SaleUpdateDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        Sale sale = await GetModelById(id);
        var updatedSale = Mapper.Map(dto, sale);
        if (dto.AddedSaleData.Count > 0)
        {
            foreach (var saleDataDto in dto.AddedSaleData)
            {
                var saleData = await GetSaleData(saleDataDto);
                updatedSale.AddSaleData(saleData);
            }
        }

        if (dto.UpdatedSaleData.Count > 0)
        {
            foreach (var saleDataDto in dto.UpdatedSaleData)
            {
                var saleData = await GetSaleData(saleDataDto);
                var oldSaleData = sale.SaleData.FirstOrDefault(s => s.ProductId == saleData.ProductId);
                if (oldSaleData != null)
                {
                    updatedSale.RemoveSaleData(oldSaleData);
                    updatedSale.AddSaleData(saleData);
                }
                else
                {
                    throw new NotFoundException($"SaleData with ProductId: {saleData.ProductId} not found");
                }
            }
        }

        if (dto.RemovedSaleData.Count > 0)
        {
            foreach (var saleDataDto in dto.RemovedSaleData)
            {
                var oldSaleData = sale.SaleData.FirstOrDefault(s => s.ProductId == saleDataDto.ProductId);
                if (oldSaleData != null)
                {
                    updatedSale.RemoveSaleData(oldSaleData);
                }
                else
                {
                    throw new NotFoundException($"SaleData with ProductId: {saleDataDto.ProductId} not found");
                }
            }
        }

        await UnitOfWork.SaleRepository.Update(updatedSale);
        await UnitOfWork.SaveChangesAsync();
    }

    private async Task<SaleData> GetSaleData(SaleDataCreateDto dto)
    {
        var product = await GetProductById(dto.ProductId);
        var saleData = Mapper.Map<SaleData>(dto);
        saleData.SetProductIdAmountByPrice(product.Price);
        return saleData;
    }

    private async Task<Product> GetProductById(long id)
    {
        Product? product = await UnitOfWork.ProductRepository.GetById(id);
        if (product == null)
            throw new NotFoundException(typeof(Product), id);
        return product;
    }
}