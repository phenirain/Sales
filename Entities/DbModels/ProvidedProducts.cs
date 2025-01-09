using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sales.Entities.DbModels;

[Owned]
public class ProvidedProduct
{
    [Required]
    public long ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}