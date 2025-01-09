namespace Sales.Entities.DbModels;

public class SalesPoint
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<ProvidedProduct> ProvidedProducts { get; set; }
}