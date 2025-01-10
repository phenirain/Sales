using Sales.Entities.DomainModels;
using Sales.Services.Dtos.CreateUpdate;

namespace Sales.Services.Dtos.Get;

public class BuyerGetDto: BuyerDto
{
    public long Id { get; set; }
    public List<long> SalesIds { get; set; }
}