using Sales.Entities.Interfaces;

namespace Sales.Entities.DomainModels;

public class Product: BaseModel
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}