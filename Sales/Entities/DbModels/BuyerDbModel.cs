using System.ComponentModel.DataAnnotations;
using Sales.Entities.DomainModels;

namespace Sales.Entities.DbModels;

public class BuyerDbModel: IGetId
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation properties
    public virtual ICollection<SaleDbModel> Sales { get; set; }
}