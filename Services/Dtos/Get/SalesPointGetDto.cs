using Sales.Services.Dtos.CreateUpdate;

namespace Sales.Services.Dtos.Get;

public class SalesPointGetDto: SalesPointCreateDto
{
    public long Id { get; set; }
}