using Microsoft.AspNetCore.Mvc;
using Sales.Controllers.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;
using Sales.Services.Interfaces;

namespace Sales.Controllers.CRUD;

[ApiController]
[Route("/crud/product")]
public class ProductCRUDController: AbstractCRUDController<ProductDto, ProductDto, ProductGetDto>
{
    public ProductCRUDController(ICRUDService<ProductDto, ProductDto, ProductGetDto> service) : base(service)
    {
    }
}