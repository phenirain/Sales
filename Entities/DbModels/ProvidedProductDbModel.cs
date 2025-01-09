using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sales.Entities.DbModels;

[Owned]
public class ProvidedProductDbModel
{
    [Required]
    public long ProductId { get; set; }
    
    [Required]
    public int ProductQuantity { get; set; }
}