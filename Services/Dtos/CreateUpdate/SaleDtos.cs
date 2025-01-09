using Sales.Services.Dtos.ValueObjectDtos;

namespace Sales.Services.Dtos.CreateUpdate;

public class SaleBaseDto
{
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public long SalesPointId { get; set; }
    public long? BuyerId { get; set; }   
}

public class SaleCreateDto: SaleBaseDto
{
    public List<SaleDataDto> SaleData { get; set; } = new List<SaleDataDto>();
}

public class SaleUpdateDto: SaleBaseDto
{
    public List<SaleDataDto> AddedSaleData { get; set; }
    public List<SaleDataDto> UpdatedSaleData { get; set; }
    public List<SaleDataDto> RemovedSaleData { get; set; }
}