using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Sales.Entities.DbModels;

public class SalesPointDbModel
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public List<ProvidedProductDbModel> ProvidedProducts { get; set; }
    
    // Navigation properties
    public virtual ICollection<SaleDbModel> Sales { get; set; }
}