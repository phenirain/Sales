namespace Sales.Entities.DbModels;

public class Sale
{
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public long SalesPointId { get; set; }
    public long BuyerId { get; set; }
    public List<SaleData> SalesData { get; set; }
}