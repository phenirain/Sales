using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sales.Entities.DbModels;

public class Sale
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public DateOnly Date { get; set; }
    
    [Required]
    public TimeOnly Time { get; set; }
    
    [Required]
    public long SalesPointId { get; set; }
    
    public long? BuyerId { get; set; }
    
    public List<SaleData> SalesData { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    // Navigation properties
    public virtual SalesPoint SalesPoint { get; set; }
    public virtual Buyer Buyer { get; set; }
}