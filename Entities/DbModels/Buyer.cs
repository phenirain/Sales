using System.ComponentModel.DataAnnotations;

namespace Sales.Entities.DbModels;

public class Buyer
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation properties
    public virtual ICollection<Sale> SalesIds { get; set; }
}