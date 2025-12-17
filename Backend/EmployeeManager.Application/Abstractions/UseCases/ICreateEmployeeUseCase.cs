using EmployeeManager.Domain.DTO.Requests;
using EmployeeManager.Domain.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Application.Abstractions.UseCases
{
    public interface ICreateEmployeeUseCase
    {
        Task<EmployeeResponseDTO> Execute(EmployeeRequestDTO content);
    }
}
