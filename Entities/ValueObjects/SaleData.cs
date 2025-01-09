namespace Sales.Entities.ValueObjects;

public class SaleData
{
    public long ProductId { get; set; }
    public int ProductQuantity { get; set; }
    public decimal ProductIdAmount { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is SaleData other)
            return ProductId == other.ProductId;
        return false;
    }
}