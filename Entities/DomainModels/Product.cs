using Sales.Entities.Interfaces;

namespace Sales.Entities.DomainModels;

public class Product: IGetId, ISetId
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}