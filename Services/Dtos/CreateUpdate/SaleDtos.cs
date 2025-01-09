using System.ComponentModel.DataAnnotations;
using Sales.Services.Dtos.ValueObjectDtos;

namespace Sales.Services.Dtos.CreateUpdate;

public class SaleBaseDto
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    [Required(ErrorMessage = "Sales point is required")]
    public long SalesPointId { get; set; }
    [Required(ErrorMessage = "Buyer id is required")]
    public long? BuyerId { get; set; }   
}

public class SaleCreateDto: SaleBaseDto
{
    public List<SaleDataCreateDto> SaleData { get; set; } = new List<SaleDataCreateDto>();
}

public class SaleUpdateDto: SaleBaseDto
{
    public List<SaleDataCreateDto> AddedSaleData { get; set; }
    public List<SaleDataCreateDto> UpdatedSaleData { get; set; }
    public List<SaleDataCreateDto> RemovedSaleData { get; set; }
}