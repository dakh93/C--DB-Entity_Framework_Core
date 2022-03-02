using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DtoModels
{
    public class ManagerDto
    {
        public ManagerDto()
        {
            this.Employees = new List<EmployeeDto>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; }

        public int EmployeesCount => this.Employees.Count;
    }
}
