using AutoMapper;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Repositories.Interfaces;
using Sales.Services.CRUD.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;

namespace Sales.Services.CRUD;

public class ProductCRUDService: AbstractCRUDService<ProductDto, ProductDto, ProductGetDto, Product, ProductDbModel>
{
    public ProductCRUDService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductCRUDService> logger)
        : base(unitOfWork, mapper, logger)
    {
    }
}