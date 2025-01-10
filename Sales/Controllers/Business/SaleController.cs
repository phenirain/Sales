using Microsoft.AspNetCore.Mvc;
using Sales.Services.Dtos.BusinessDtos.SaleDtos;
using Sales.Services.Interfaces;

namespace Sales.Controllers.Business;

[ApiController]
[Route("sale")]
public class SaleController: ControllerBase
{
    private readonly ISaleService _saleService;
    
    public SaleController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpPost("/sale")]
    public async Task<SaleResponse> Sale(SaleRequest request)
    {
        var stringBuyerId = HttpContext.User.FindFirst("BuyerId")?.Value;
        return await _saleService.Sale(request, stringBuyerId: stringBuyerId);
    }
}