using Sales.Entities.ValueObjects;

namespace Sales.Entities.DomainModels;

public class SalesPoint: BaseModel
{
    public string Name { get; private set; }
    public List<ProvidedProduct> ProvidedProducts { get; private set; }
    
    public SalesPoint(string name)
    {
        Name = name;
        ProvidedProducts = new List<ProvidedProduct>();
    }

    public void AddProvidedProduct(ProvidedProduct pp)
    {
        if (pp == null)
            throw new ArgumentNullException(nameof(pp));
        ProvidedProduct? oldPp = ProvidedProducts.FirstOrDefault(p => p.ProductId == pp.ProductId);
        if (oldPp != null)
        {
            pp = new ProvidedProduct(pp.ProductId, pp.ProductQuantity + oldPp.ProductQuantity);
            ProvidedProducts.Remove(oldPp);        
        }
            
        ProvidedProducts.Add(pp);
    }

    public void RemoveProvidedProduct(ProvidedProduct pp)
    {
        if (pp == null)
            throw new ArgumentNullException(nameof(pp));
        if (ProvidedProducts.Contains(pp))
            ProvidedProducts.Remove(pp);
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