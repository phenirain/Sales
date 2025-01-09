using Sales.Entities.ValueObjects;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.ValueObjectDtos;

namespace Sales.Services.Dtos.Get;

public class SaleGetDto: SaleBaseDto
{
    public long Id { get; set; }
    public decimal TotalAmount { get; set; }
    public List<SaleDataGetDto> SaleData { get; set; } = new List<SaleDataGetDto>();
}