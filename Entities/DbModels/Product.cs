using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Entities.DbModels;

public class Product
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
}