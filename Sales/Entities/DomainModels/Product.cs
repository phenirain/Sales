
namespace Sales.Entities.DomainModels;

public class Product: BaseModel
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}