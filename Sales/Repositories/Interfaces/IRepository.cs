namespace Sales.Repositories.Interfaces;

public interface IRepository<Model>
{
    Task<IEnumerable<Model>> GetAll(int limit, int offset);
    Task<Model?> GetById(long id);
    Task<long> Create(Model model);
    Task Update(Model model);
    Task Delete(Model model);
}