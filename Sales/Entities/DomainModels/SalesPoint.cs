using Sales.Entities.Interfaces;
using Sales.Entities.ValueObjects;

namespace Sales.Entities.DomainModels;

public class SalesPoint: BaseModel
{
    public string Name { get; set; }
    public List<ProvidedProduct> ProvidedProducts { get; set; }
}