using System.ComponentModel.DataAnnotations;

namespace Sales.Services.Dtos.BusinessDtos.SaleDtos;

public class SaleRequest
{
    [Required(ErrorMessage = "Sales point is required")]
    public long SalesPointId { get; set; }
    [Required(ErrorMessage = "Product id is required")]
    public long ProductId { get; set; }
    [Required(ErrorMessage = "Product quantity is required")]
    [Range(0, Int32.MaxValue, ErrorMessage = "Product quantity must be greater than 0")]
    public int ProductQuantity { get; set; }
}