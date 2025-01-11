using Sales.Entities.ValueObjects;

namespace Sales.Entities.DomainModels;

public class Sale: BaseModel
{
    public DateOnly Date { get; private set; }
    public TimeOnly Time { get; private set; }
    public long SalesPointId { get; set; }
    public long? BuyerId { get; set; }
    private List<SaleData> _saleData { get; } = new List<SaleData>();
    private decimal _totalAmount { get; set; }

    public Sale(long salesPointId, long? buyerId = null, DateOnly? date = null, TimeOnly? time = null)
    {
        Date = date ?? DateOnly.FromDateTime(DateTime.Now);
        Time = time?? TimeOnly.FromDateTime(DateTime.Now);
        SalesPointId = salesPointId;
        BuyerId = buyerId;
    }
    
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
        if (saleData == null)
            throw new ArgumentNullException(nameof(saleData));
        
        _totalAmount += saleData.ProductIdAmount;
        _saleData.Add(saleData);
    }
    
    public void RemoveSaleData(SaleData saleData)
    {
        if (!_saleData.Contains(saleData))
            throw new InvalidOperationException($"Sale data with productId {saleData.ProductId} not found in the sale id: {Id}.");
        _totalAmount -= saleData.ProductIdAmount;
        _saleData.Remove(saleData);
    }

    public void AddSaleDataRange(List<SaleData> saleDataRange)
    {
        _totalAmount = saleDataRange.Sum(s => s.ProductIdAmount);
        _saleData.AddRange(saleDataRange);
    }
    
}