using Employees.App.Command.Contracts;
using Employees.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.App.Command
{
    internal class ManagerInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public ManagerInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public string Execute(params string[] args)
        {
            var managerId = int.Parse(args[0]);

            var m = employeeService.ManagerInfo(managerId);

            if (m == null) return $"There is no Manager with id {managerId}";

            var sb = new StringBuilder();

            sb.AppendLine($"{m.FirstName} {m.LastName} | Employees: {m.EmployeesCount} ");
            foreach (var emp in m.Employees)
            {
                sb.AppendLine($"    - {emp.FirstName} {emp.LastName} - ${emp.Salary:F2}");
            }

            return sb.ToString();
        }
    }
}
