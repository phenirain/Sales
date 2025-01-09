using Sales.Services.Dtos.CreateUpdate;

namespace Sales.Services.Dtos.Get;

public class ProductGetDto: ProductDto
{
    public long Id { get; set; }
}