using Microsoft.AspNetCore.Mvc;
using Sales.Controllers.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;
using Sales.Services.Interfaces;

namespace Sales.Controllers.CRUD;

[ApiController]
[Route("/crud/buyer")]
public class BuyerCRUDController: AbstractCRUDController<BuyerDto, BuyerDto, BuyerGetDto>
{
    public BuyerCRUDController(ICRUDService<BuyerDto, BuyerDto, BuyerGetDto> service) : base(service)
    {
    }
}