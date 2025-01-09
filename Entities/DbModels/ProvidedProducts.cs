using Microsoft.EntityFrameworkCore;

namespace Sales.Entities.DbModels;

[Owned]
public class ProvidedProduct
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
}