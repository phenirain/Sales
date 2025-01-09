namespace Sales.Entities.DbModels;

public class Buyer
{
    public long Id { get; set; }
    public string Name { get; set; }

    public List<Sale> SalesIds { get; set; }
}