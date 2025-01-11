using AutoMapper;
using Sales.Entities.DbModels;
using Sales.Entities.DomainModels;
using Sales.Entities.ValueObjects;

namespace Sales.Entities.Mapping;

public class EntityMapperProfile: Profile
{
    public EntityMapperProfile()
    {
        CreateBuyerMapping();
        CreateProductMapping();
        CreateProvidedProductMapping();
        CreateSalesPointMapping();
        CreateSaleDataMapping();
        CreateSaleMapping();
    }

    public void CreateBuyerMapping()
    {
        CreateMap<BuyerDbModel, Buyer>()
            .ConstructUsing(src => new Buyer(src.Name, src.Sales.Select(s => s.Id)));
        CreateMap<Buyer, BuyerDbModel>();
    }

    public void CreateProductMapping()
    {
        CreateMap<ProductDbModel, Product>()
            .ConstructUsing(src => new Product(src.Name, src.Price));
        CreateMap<Product, ProductDbModel>();
    }

    public void CreateProvidedProductMapping()
    {
        CreateMap<ProvidedProductDbModel, ProvidedProduct>()
            .ConstructUsing(src => new ProvidedProduct(src.ProductId, src.ProductQuantity));
        CreateMap<ProvidedProduct, ProvidedProductDbModel>();
    }

    public void CreateSalesPointMapping()
    {
        CreateMap<SalesPointDbModel, SalesPoint>()
            .ConstructUsing(src => new SalesPoint(src.Name))
            .AfterMap((src, sp) =>
            {
                foreach (var pp in src.ProvidedProducts)
                {
                    var providedProduct = new ProvidedProduct(pp.ProductId, pp.ProductQuantity);
                    sp.AddProvidedProduct(providedProduct);
                }
            });
        CreateMap<SalesPoint, SalesPointDbModel>();
    }

    public void CreateSaleDataMapping()
    {
        CreateMap<SaleDataDbModel, SaleData>()
            .ConstructUsing(src => new SaleData(src.ProductId, src.ProductQuantity))
            .AfterMap((src, sd) => sd.SetProductIdAmountByPrice(src.ProductIdAmount));
        CreateMap<SaleData, SaleDataDbModel>();
    }

    public void CreateSaleMapping()
    {
        CreateMap<SaleDbModel, Sale>()
            .ConstructUsing(src => new Sale(src.SalesPointId, src.BuyerId, src.Date, src.Time))
            .AfterMap((src, sale) => {
                var saleData = src.SaleData.Select(sd =>
                {
                    var saleData = new SaleData(sd.ProductId, sd.ProductQuantity);
                    saleData.SetProductIdAmountByPrice(sd.ProductIdAmount);
                    return saleData;
                }).ToList();
                sale.AddSaleDataRange(saleData);
            });
        CreateMap<Sale, SaleDbModel>();
    }
}