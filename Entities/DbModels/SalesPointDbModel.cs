using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Sales.Entities.DomainModels;

namespace Sales.Entities.DbModels;

public class SalesPointDbModel: IGetId
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public List<ProvidedProductDbModel> ProvidedProducts { get; set; }
    
    // Navigation properties
    public virtual ICollection<SaleDbModel> Sales { get; set; }
}