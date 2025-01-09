using System.ComponentModel.DataAnnotations;

namespace Sales.Entities.DbModels;

public class BuyerDbModel
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation properties
    public virtual ICollection<SaleDbModel> Sales { get; set; }
}