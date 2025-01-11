using Sales.Entities.ValueObjects;

namespace Sales.Entities.DomainModels;

public class SalesPoint: BaseModel
{
    public string Name { get; private set; }
    
    public IReadOnlyList<ProvidedProduct> ProvidedProducts
    {
        get => _providedProducts;
    }
    public List<ProvidedProduct> _providedProducts;
    
    public SalesPoint(string name)
    {
        Name = name;
        _providedProducts = new List<ProvidedProduct>();
    }

    public void AddProvidedProduct(ProvidedProduct pp)
    {
        if (pp == null)
            throw new ArgumentNullException(nameof(pp));
        ProvidedProduct? oldPp = _providedProducts.FirstOrDefault(p => p.ProductId == pp.ProductId);
        if (oldPp != null)
        {
            pp = new ProvidedProduct(pp.ProductId, pp.ProductQuantity + oldPp.ProductQuantity);
            _providedProducts.Remove(oldPp);        
        }
            
        _providedProducts.Add(pp);
    }

    public void RemoveProvidedProduct(ProvidedProduct pp)
    {
        if (pp == null)
            throw new ArgumentNullException(nameof(pp));
        if (_providedProducts.Contains(pp))
            _providedProducts.Remove(pp);
    }

    public void AddProvidedProductRange(List<ProvidedProduct> pps)
    {
        if (pps == null)
            throw new ArgumentNullException(nameof(pps));
        foreach (var pp in pps)
        {
            AddProvidedProduct(pp);
        }
    }

}