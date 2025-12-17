using Dapper;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Infrastructure.Commands;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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

            Guid id = await _connection.ExecuteScalarAsync<Guid>(EmployeeCommands.CreateCommand, entity);

            return id.ToString();
        }

        public async Task Delete(string id)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            await _connection.ExecuteScalarAsync(EmployeeCommands.DeleteCommand, new { id = Guid.Parse(id) });
        }

        public async Task<Employee?> Get(string id)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            Employee? employee = await _connection.QuerySingleOrDefaultAsync<Employee>(EmployeeCommands.QueryCommand, new { id = Guid.Parse(id) });

            return employee;
        }

        public async Task<List<Employee>> Get()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            IEnumerable<Employee>? employees = await _connection.QueryAsync<Employee>(EmployeeCommands.QueryAllCommand);

            return employees.ToList();
        }

        public async Task Update(Employee entity)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            await _connection.ExecuteScalarAsync(EmployeeCommands.UpdateCommand, new { id = entity.GetId(), firstname = entity.FirstName, lastname = entity.LastName, mail = entity.Mail, positionName = entity.PositionName });
        }
    }
}
