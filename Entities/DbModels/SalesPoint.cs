using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sales.Entities.DbModels;

public class SalesPoint
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public List<ProvidedProduct> ProvidedProducts { get; set; }
    
    // Navigation properties
    public virtual ICollection<Sale> Sales { get; set; }
}