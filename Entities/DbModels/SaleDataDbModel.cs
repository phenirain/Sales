using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sales.Entities.DbModels;

[Owned]
public class SaleDataDbModel
{
    [Required]
    public long ProductId { get; set; }
    
    [Required]
    public int ProductQuantity { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ProductIdAmount { get; set; }
}