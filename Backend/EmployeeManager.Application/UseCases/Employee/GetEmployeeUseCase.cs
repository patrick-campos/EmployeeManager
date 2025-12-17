using EmployeeManager.Application.Abstractions.UseCases;
using EmployeeManager.Domain.DTO.Responses;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;

namespace EmployeeManager.Application.UseCases
{
    public class GetEmployeeUseCase : IGetEmployeeUseCase
    {
        private readonly IRepository<Employee> _repository;

        public GetEmployeeUseCase(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeResponseDTO> Execute(string IdOfUser)
        {
            Employee? empl = await _repository.Get(IdOfUser);

            if(empl is null)
                throw new ArgumentNullException("There's not any user with this argument");

            return new EmployeeResponseDTO(empl);

        }

        public async Task<List<EmployeeResponseDTO>> Execute()
        {
            List<Employee> empls = await _repository.Get();

            return empls.Select(emp => new EmployeeResponseDTO(emp)).ToList();

        }
    }
}
