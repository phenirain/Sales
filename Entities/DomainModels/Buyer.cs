namespace Sales.Entities.DomainModels;

public class Buyer
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<long> SalesIds { get; set; }
}