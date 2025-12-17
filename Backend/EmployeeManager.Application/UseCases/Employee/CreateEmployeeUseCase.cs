using EmployeeManager.Application.Abstractions.UseCases;
using EmployeeManager.Domain.DTO.Requests;
using EmployeeManager.Domain.DTO.Responses;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.UseCases
{
    public class CreateEmployeeUseCase : ICreateEmployeeUseCase
    {
        public IRepository<Employee> _repository;

        public CreateEmployeeUseCase(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<EmployeeResponseDTO> Execute(EmployeeRequestDTO content)
        {
            Employee newEmp = new Employee(content);

            string idOfNewEmployee = await _repository.Create(newEmp);

            return new EmployeeResponseDTO(newEmp, idOfNewEmployee);

        }
    }
}
