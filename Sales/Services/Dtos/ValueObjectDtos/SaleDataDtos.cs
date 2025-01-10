using System.ComponentModel.DataAnnotations;

namespace Sales.Services.Dtos.ValueObjectDtos;

public class SaleDataCreateDto
{
    [Required(ErrorMessage = "Product id is required")]
    public long ProductId { get; set; }
    [Required(ErrorMessage = "Product quantity is required")]
    [Range(0, Int32.MaxValue, ErrorMessage = "Product quantity must be greater than zero")]
    public int ProductQuantity { get; set; }
}
     
    
    
public class SaleDataGetDto: SaleDataCreateDto
{
    public decimal ProductIdAmount { get; set; }
}

