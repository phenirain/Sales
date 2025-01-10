using System.ComponentModel.DataAnnotations;
using Sales.Exceptions;

namespace Sales.Services.Dtos.CreateUpdate;

public class ProductDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
}