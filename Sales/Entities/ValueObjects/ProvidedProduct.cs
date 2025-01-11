using Sales.Exceptions;

namespace Sales.Entities.ValueObjects;

public class ProvidedProduct
{
    public long ProductId { get; private set; }
    public int ProductQuantity { get; private set; }

    public ProvidedProduct(long productId, int productQuantity)
    {
        ProductId = productId;
        ProductQuantity = productQuantity;
    }
    
    public void AddProduct(int productQuantity)
    {
        ProductQuantity += productQuantity;
    }

    public void SellProduct(int productQuantity)
    {
        if (ProductQuantity < productQuantity)
            throw new OutOfStockException(ProductId, ProductQuantity, productQuantity);

        ProductQuantity -= productQuantity;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is ProvidedProduct providedProduct)
        {
            return ProductId == providedProduct.ProductId;
        }
        return false;
    }
}