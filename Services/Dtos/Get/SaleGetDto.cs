using Sales.Entities.ValueObjects;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.ValueObjectDtos;

namespace Sales.Services.Dtos.Get;

public class SaleGetDto: SaleCreateDto
{
    public long Id { get; set; }
    public decimal TotalAmount { get; set; }
}