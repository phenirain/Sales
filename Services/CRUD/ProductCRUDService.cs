using AutoMapper;
using Sales.Entities.DomainModels;
using Sales.Repositories.Interfaces;
using Sales.Services.CRUD.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;

namespace Sales.Services.CRUD;

public class ProductCRUDService: AbstractCRUDService<ProductDto, ProductDto, ProductGetDto, Product>
{
    public ProductCRUDService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}