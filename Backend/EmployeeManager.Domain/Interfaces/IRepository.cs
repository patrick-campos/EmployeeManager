

namespace EmployeeManager.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<String> Create(T entity);
    }
}
