using Employees.App.Command.Contracts;
using Employees.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.App.Command
{
    internal class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public ListEmployeesOlderThanCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var age = int.Parse(args[0]);

            var employeesProjectionsDtos = employeeService.ListEmployeesOlderThan(age);

            var sb = new StringBuilder();

            foreach (var e in employeesProjectionsDtos)
            {
                sb.AppendLine(e.ToString());
            }

            return sb.ToString();
        }
    }
}
