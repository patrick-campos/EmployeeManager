using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.Dto
{
    public class EmployeeDto
    {
        public String Id { get; private set; }
        public String Name { get; private set; }
        public String Email { get; private set; }
        public String Position { get; private set; }

        public EmployeeDto(Employee employee)
        {
            this.Id = employee.GetId();
            this.Name = $"{employee.FirstName} {employee.LastName}";
            this.Email = employee.Email;
            this.Position = employee.Position.ToString();
        }
    }
}
