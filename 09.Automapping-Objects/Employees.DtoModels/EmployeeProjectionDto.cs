using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DtoModels
{
    public class EmployeeProjectionDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal Salary { get; set; }
        public Employee? Manager { get; set; }

        public override string ToString()
        {
            var manager = "[no manager]";

            if (this.Manager != null)
            {
                manager = this.Manager.FirstName + this.Manager.LastName;
            }

            return $"{this.FirstName} {this.LastName} - ${this.Salary:F2} - Manager: {manager}";
        }
    }
}
