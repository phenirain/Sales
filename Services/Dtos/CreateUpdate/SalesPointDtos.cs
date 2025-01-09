using Sales.Services.Dtos.ValueObjectDtos;

namespace Sales.Services.Dtos.CreateUpdate;

public class SalesPointBaseDto
{
    public string Name { get; set; }
}

public class SalesPointCreateDto: SalesPointBaseDto
{
    public List<ProvidedProductDto> ProvidedProducts { get; set; } = new List<ProvidedProductDto>();
}

public class SalesPointUpdateDto: SalesPointBaseDto
{
    public List<ProvidedProductDto> AddedProvidedProducts { get; set; }
    public List<ProvidedProductDto> UpdatedProvidedProducts { get; set; }
    public List<ProvidedProductDto> RemovedProvidedProducts { get; set; }
}