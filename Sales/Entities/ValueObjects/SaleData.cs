namespace Sales.Entities.ValueObjects;

public class SaleData
{
    public long ProductId { get; private set; }
    public int ProductQuantity { get; private set; }
    private decimal _productIdAmount;
    public decimal ProductIdAmount
    {
        get => _productIdAmount;
    }

    public SaleData(long productId, int productQuantity)
    {
        ProductId = productId;
        SetProductQuantity(productQuantity);
    }

    private void SetProductQuantity(int productQuantity)
    {
        if (productQuantity <= 0)
        {
            throw new ArgumentException("Product quantity must be greater than zero");
        }

        ProductQuantity = productQuantity;
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