using Sales.Entities.ValueObjects;

namespace Sales.Entities.DomainModels;

public class SalesPoint: IGetId
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<ProvidedProduct> ProvidedProducts { get; set; }
}