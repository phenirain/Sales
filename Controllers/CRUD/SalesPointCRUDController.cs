using Microsoft.AspNetCore.Mvc;
using Sales.Controllers.Abstract;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;
using Sales.Services.Interfaces;

namespace Sales.Controllers.CRUD;

[ApiController]
[Route("/crud/salesPoint")]
public class SalesPointCRUDController: AbstractCRUDController<SalesPointCreateDto, SalesPointUpdateDto, SalesPointGetDto>
{
    public SalesPointCRUDController(ICRUDService<SalesPointCreateDto, SalesPointUpdateDto, SalesPointGetDto> service) : base(service)
    {
    }
}