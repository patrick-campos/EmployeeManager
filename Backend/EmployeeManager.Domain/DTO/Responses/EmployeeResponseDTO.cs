using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.DTO.Responses
{
    public class EmployeeResponseDTO
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Position { get; private set; }

        public EmployeeResponseDTO(Employee employee)
        {
            Id = employee.GetId();
            Name = $"{employee.FirstName} {employee.LastName}";
            Email = employee.Email;
            Position = employee.Position.ToString();
        }
    }
}
