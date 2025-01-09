using Sales.Services.Dtos.BusinessDtos.SaleDtos;

namespace Sales.Services.Interfaces;

public interface ISaleService
{
    Task<SaleResponse> Sale(SaleRequest request);
}