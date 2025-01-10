using Microsoft.AspNetCore.Mvc;
using Sales.Services.Interfaces;

namespace Sales.Controllers.Abstract;

public abstract class AbstractCRUDController<CreateDto, UpdateDto, GetDto>: ControllerBase
{
    protected readonly ICRUDService<CreateDto, UpdateDto, GetDto> _service;
    
    public AbstractCRUDController(ICRUDService<CreateDto, UpdateDto, GetDto> service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IEnumerable<GetDto>> GetAll(int limit, int offset)
    {
        return await _service.GetAll(limit, offset);
    }

    [HttpGet("{id}")]
    public async Task<GetDto> GetById(long id)
    {
        return await _service.GetById(id);
    }

    [HttpPost]
    public async Task<long> Create(CreateDto createDto)
    {
        return await _service.Create(createDto);
    }

    [HttpPut("{id}")]
    public async Task Update(long id, UpdateDto updateDto)
    {
        await _service.Update(id, updateDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete(long id)
    {
        await _service.Delete(id);
    }
}