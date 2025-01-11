
namespace Sales.Entities.DomainModels;

public class Buyer: BaseModel
{
    public Buyer(string name, IEnumerable<long> salesIds)
    {
        Name = name;
        SalesIds = salesIds.ToList();
    }
    
    public string Name { get; private set; }
    public List<long> SalesIds { get; private set; }
}