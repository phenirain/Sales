using Microsoft.AspNetCore.Mvc;
using Sales.Controllers.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;
using Sales.Services.Interfaces;

namespace Sales.Controllers.CRUD;


[ApiController]
[Route("/crud/sale")]
public class SaleCRUDController: AbstractCRUDController<SaleCreateDto, SaleUpdateDto, SaleGetDto>
{
    public SaleCRUDController(ICRUDService<SaleCreateDto, SaleUpdateDto, SaleGetDto> service) : base(service)
    {
    }
}