

namespace EmployeeManager.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> Get(string id);
        Task<List<T>> Get();
        Task<String> Create(T entity);
        Task Delete(string id);
        Task Update(T entity);
    }
}
