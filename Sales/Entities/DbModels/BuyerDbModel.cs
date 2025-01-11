using System.ComponentModel.DataAnnotations;
using Sales.Entities.DomainModels;

namespace Sales.Entities.DbModels;

public class BuyerDbModel: BaseDbModel
{

    [Required]
    public string Name { get; set; }

    // Navigation properties
    public virtual ICollection<SaleDbModel> Sales { get; set; }
}