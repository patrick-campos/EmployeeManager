using Dapper;
using EmployeeManager.Domain.Dto;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Infrastructure.Commands;
using System.Data;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly IDbConnection _connection;

        public EmployeeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<String> Create(Employee entity)
        {
            if(_connection.State != ConnectionState.Open)
                _connection.Open();

            return await _connection.ExecuteScalarAsync<String>(EmployeeCommands.CreateCommand, entity);
        }

        public async Task<Employee> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
