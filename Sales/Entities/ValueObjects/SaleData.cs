namespace Sales.Entities.ValueObjects;

public class SaleData
{
    public long ProductId { get; set; }
    public int ProductQuantity { get; set; }
    private decimal _productIdAmount;
    public decimal ProductIdAmount
    {
        get => _productIdAmount;
    }

    public SaleData(long productId, int productQuantity, decimal productPrice)
    {
        ProductId = productId;
        ProductQuantity = productQuantity;
        SetProductIdAmountByPrice(productPrice);
    }

    public void SetProductIdAmountByPrice(decimal productPrice)
    {
        _productIdAmount = ProductQuantity * productPrice;
    } 

    public override bool Equals(object obj)
    {
        if (obj is SaleData other)
            return ProductId == other.ProductId;
        return false;
    }
}