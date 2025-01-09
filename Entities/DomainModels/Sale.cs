using Sales.Entities.Interfaces;
using Sales.Entities.ValueObjects;

namespace Sales.Entities.DomainModels;

public class Sale: IGetId, ISetId
{
    public long Id { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public TimeOnly Time { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
    public long SalesPointId { get; set; }
    public long? BuyerId { get; set; }
    private List<SaleData> _saleData { get; } = new List<SaleData>();
    private decimal _totalAmount { get; set; }
    
    public decimal TotalAmount
    {
        get => _totalAmount;
    }

    public IReadOnlyList<SaleData> SaleData
    {
        get => _saleData.AsReadOnly();
    }

    public void AddSaleData(SaleData saleData)
    {
        _totalAmount += saleData.ProductIdAmount;
        _saleData.Add(saleData);
    }
    
    public void RemoveSaleData(SaleData saleData)
    {
        _totalAmount -= saleData.ProductIdAmount;
        _saleData.Remove(saleData);
    }

    public void AddSaleDataRange(List<SaleData> saleDataRange)
    {
        _totalAmount = saleDataRange.Sum(s => s.ProductIdAmount);
        _saleData.AddRange(saleDataRange);
    }
    
}