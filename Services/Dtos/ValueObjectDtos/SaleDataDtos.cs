namespace Sales.Services.Dtos.ValueObjectDtos;

public class SaleDataCreateDto
{
    public long ProductId { get; set; }
    public int ProductQuantity { get; set; }
}
     
    
    
public class SaleDataGetDto: SaleDataCreateDto
{
    public decimal ProductIdAmount { get; set; }
}

