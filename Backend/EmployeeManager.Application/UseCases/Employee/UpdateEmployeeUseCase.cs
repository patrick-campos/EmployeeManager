using EmployeeManager.Application.Abstractions.UseCases;
using EmployeeManager.Domain.DTO.Requests;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Application.UseCases
{
    public class UpdateEmployeeUseCase : IUpdateEmployeeUseCase
    {
        private readonly IRepository<Employee> _repository;

        public UpdateEmployeeUseCase(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task Execute(EmployeeRequestDTO content, string id)
        {
            Employee? empl = await _repository.Get(id);

            if(empl is null)
                throw new ArgumentNullException("There's not user with this argument");

            empl.update(content);

            await _repository.Update(empl);

        }
    }
}
