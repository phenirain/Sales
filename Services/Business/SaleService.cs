using AutoMapper;
using Sales.Entities.DomainModels;
using Sales.Entities.ValueObjects;
using Sales.Exceptions;
using Sales.Repositories.Interfaces;
using Sales.Services.Dtos.BusinessDtos.SaleDtos;
using Sales.Services.Interfaces;

namespace Sales.Services.Business;

public class SaleService: ISaleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    
    public async Task<SaleResponse> Sale(SaleRequest request, long? buyerId = null)
    {
        SalesPoint? salesPoint = await _unitOfWork.SalesPointRepository.GetById(request.SalesPointId);
        if (salesPoint == null)
            throw new NotFoundException(typeof(SalesPoint), request.SalesPointId);

        ProvidedProduct? providedProduct = salesPoint.ProvidedProducts.FirstOrDefault(pp => pp.ProductId == request.ProductId);
        if (providedProduct == null)
            throw new NotFoundException($"Product with id : {request.ProductId} not found at sales point with id : {request.SalesPointId}");
        if (providedProduct.ProductQuantity <= request.ProductQuantity)
        {
            throw new OutOfStockException(request.ProductId, providedProduct.ProductQuantity, request.ProductQuantity);
        }

        Product product = await _unitOfWork.ProductRepository.GetById(providedProduct.ProductId);

        await _unitOfWork.BeginTransactionAsync();
        
        providedProduct.ProductQuantity -= request.ProductQuantity;
        SaleData saleData = new SaleData
        (
            productId: request.ProductId,
            productQuantity: request.ProductQuantity,
            productPrice: product!.Price
        );
        Sale sale = new Sale
        {
            SalesPointId = request.SalesPointId,
            BuyerId = buyerId
        };
        sale.AddSaleData(saleData);

        await _unitOfWork.SalesPointRepository.Update(salesPoint);
        await _unitOfWork.SaleRepository.Create(sale);

        await _unitOfWork.CommitAsync();

        return new SaleResponse
        {
            Status = true,
            Message = "Sale successful"
        };
    }
}