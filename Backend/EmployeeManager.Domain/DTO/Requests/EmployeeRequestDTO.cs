using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Domain.DTO.Requests
{
    public class EmployeeRequestDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string DocumentNumber { get; set; }
        public string PositionName { get; set; }
        public string PositionId { get; set; }
        public string DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        
        
    }
}
