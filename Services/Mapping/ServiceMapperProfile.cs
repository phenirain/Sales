using AutoMapper;
using Sales.Entities.DomainModels;
using Sales.Entities.ValueObjects;
using Sales.Services.Dtos.CreateUpdate;
using Sales.Services.Dtos.Get;
using Sales.Services.Dtos.ValueObjectDtos;

namespace Sales.Services.Mapping;

public class ServiceMapperProfile: Profile
{

    public ServiceMapperProfile()
    {
        CreateProvidedProductsMapping();
        CreateSaleDataMapping();
        CreateBuyerMapping();
        CreateProductMapping();
        CreateSaleMapping();
        CreateSalesPointMapping();
    }

    public void CreateProvidedProductsMapping()
    {
        CreateMap<ProvidedProductDto, ProvidedProduct>();
        CreateMap<ProvidedProduct, ProvidedProductDto>();
    }

    public void CreateSaleDataMapping()
    {
        CreateMap<SaleDataDto, SaleData>();
        CreateMap<SaleData, SaleDataDto>();
    }

    public void CreateBuyerMapping()
    {
        CreateMap<Buyer, BuyerGetDto>();
        CreateMap<BuyerDto, Buyer>();
    }

    public void CreateProductMapping()
    {
        CreateMap<Product, ProductGetDto>();
        CreateMap<ProductDto, Product>();
    }

    public void CreateSaleMapping()
    {
        CreateMap<Sale, SaleGetDto>();
        CreateMap<SaleCreateDto, Sale>();
        CreateMap<SaleUpdateDto, Sale>();
    }
    
    public void CreateSalesPointMapping()
    {
        CreateMap<SalesPoint, SalesPointGetDto>();
        CreateMap<SalesPointCreateDto, SalesPoint>();
        CreateMap<SalesPointUpdateDto, SalesPoint>();
    }
}