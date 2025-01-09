namespace Sales.Services.Interfaces;

public interface ICRUDService<CreateDto, UpdateDto, GetDto>
{
    Task<IEnumerable<GetDto>> GetAll(int limit, int offset);
    Task<GetDto> GetById(long id);
    Task<long> Create(CreateDto createDto);
    Task Update(long id, UpdateDto updateDto);
    Task Delete(long id);
}