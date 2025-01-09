using Microsoft.EntityFrameworkCore;

namespace Sales.Entities.DbModels;

[Owned]
public class SaleData
{
    public long ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ProductIdAmount { get; set; }
}