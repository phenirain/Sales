using Sales.Entities.Interfaces;

namespace Sales.Entities.DomainModels;

public class Buyer: BaseModel
{
    public string Name { get; set; }
    public List<long> SalesIds { get; set; }
}