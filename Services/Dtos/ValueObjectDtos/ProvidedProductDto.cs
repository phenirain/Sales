namespace Sales.Services.Dtos.ValueObjectDtos;

public class ProvidedProductDto
{
    public long ProductId { get; set; }
    public int ProductQuantity { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is ProvidedProductDto other)
            return ProductId == other.ProductId;
        return false;
    }
}