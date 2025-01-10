namespace Sales.Repositories.Interfaces;

public interface IRepository<Model, DbModel>
{
    Task<IEnumerable<Model>> GetAll(int limit, int offset);
    Task<Model?> GetById(long id);
    Task<DbModel> Create(Model model);
    Task Update(Model model);
    Task Delete(Model model);
}