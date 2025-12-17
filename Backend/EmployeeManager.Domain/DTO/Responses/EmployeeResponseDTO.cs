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
        public string Lastname { get; private set; }
        public string Email { get; private set; }
        public string Position { get; private set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }

        public EmployeeResponseDTO(Employee employee, string? id = null)
        {
            Id = id ?? employee.GetId().ToString();
            Name = employee.FirstName;
            Lastname = employee.LastName;
            Email = employee.Mail;
            Position = employee.PositionName;
            DocumentNumber = employee.DocumentNumber;
            DocumentType = employee.DocumentType;
        }
    }
}
