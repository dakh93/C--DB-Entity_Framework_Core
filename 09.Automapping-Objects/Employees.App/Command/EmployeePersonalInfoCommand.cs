using Employees.App.Command.Contracts;
using Employees.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.App.Command
{
    internal class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //EmployeePersonalInfo<employeeId>
        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var employeeInfo = employeeService.PersonalById(employeeId);

            var birthday = "[no birthday specified]";
            if (employeeInfo.BirthDay != null)
            {
                birthday =  employeeInfo.BirthDay.Value.ToString("dd-MM-yyyy");
            }

            string address = employeeInfo.Address ?? "[no address speciafied]";

            var sb = new StringBuilder();

            sb.AppendLine($"ID: {employeeId} - {employeeInfo.FirstName} {employeeInfo.LastName} - ${employeeInfo.Salary:F2}");
            sb.AppendLine($"Birthday: {birthday}");
            sb.AppendLine($"Address: {address}");

            return sb.ToString();
            
        }
    }
}
