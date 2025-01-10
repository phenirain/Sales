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
        CreateMap<BuyerDbModel, Buyer>();
        CreateMap<Buyer, BuyerDbModel>();
    }

    public void CreateProductMapping()
    {
        CreateMap<ProductDbModel, Product>();
        CreateMap<Product, ProductDbModel>();
    }

    public void CreateProvidedProductMapping()
    {
        CreateMap<ProvidedProductDbModel, ProvidedProduct>();
        CreateMap<ProvidedProduct, ProvidedProductDbModel>();
    }

    public void CreateSalesPointMapping()
    {
        CreateMap<SalesPointDbModel, SalesPoint>();
        CreateMap<SalesPoint, SalesPointDbModel>();
    }

    public void CreateSaleDataMapping()
    {
        CreateMap<SaleDataDbModel, SaleData>();
        CreateMap<SaleData, SaleDataDbModel>();
    }

    public void CreateSaleMapping()
    {
        CreateMap<SaleDbModel, Sale>();
        CreateMap<Sale, SaleDbModel>();
    }
}