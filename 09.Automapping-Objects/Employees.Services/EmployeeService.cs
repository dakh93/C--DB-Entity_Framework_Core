namespace Employees.Services
{
    using AutoMapper;
    using System.Text;
    using System.Linq;
   

    using Employees.Data;
    using Employees.DtoModels;
    using Employees.Models;

    public class EmployeeService
    {
        private readonly EmployeesContext context;
        private readonly IMapper mapper;

        public EmployeeService(EmployeesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = this.context.Employees.Find(employeeId);

            var employeeDto = mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = mapper.Map<Employee>(dto);

            context.Employees.Add(employee);
            context.SaveChanges();

        }

        public string SetBirthday(int employeeId, DateTime date)
        {
            var employee = context.Employees
                .Find(employeeId);

            employee.BirthDay = date;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = context.Employees
                .Find(employeeId);

            employee.Address = address;

            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string GetEmployeeInfo(int employeeId)
        {
            var employee = context.Employees
                .Find(employeeId);

            var sb = new StringBuilder();
            sb.AppendLine($"ID: {employee.Id} - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}");

            sb.AppendLine($"Birthday: {employee.BirthDay}");
            sb.AppendLine($"Address: {employee.Address}");

            return sb.ToString().Trim();
        }

        public EmployeePersonalDto PersonalById(int employeeId)
        {
            var employee = this.context.Employees.Find(employeeId);

            var employeeDto = mapper.Map<EmployeePersonalDto>(employee);

            return employeeDto;
        }

        public string SetManager(int employeeId, int managerId)
        {
            var employee = this.context.Employees.Find(employeeId);
            var manager = this.context.Employees.Find(managerId);

            if (employee == null) return $"There is no Employee with id {employeeId}";
            if (manager == null) return $"There is no Manager with id {managerId}";

            employee.Manager = manager;
            employee.ManagerId = managerId;

            manager.Employees.Add(employee);

            this.context.SaveChanges();

            return $"{manager.FirstName} {manager.LastName} is set to be manager to {employee.FirstName} {employee.LastName}";
        }

        public ManagerDto ManagerInfo(int managerId)
        {
            var manager = this.context.Employees.Find(managerId);

            var managerDto = mapper.Map<ManagerDto>(manager);

            return managerDto;
        }

        public List<EmployeeProjectionDto> ListEmployeesOlderThan(int age)
        {
            var employees = this.context.Employees
                .Where(e => DateTime.Now.Year - e.BirthDay.Value.Year > age)
                
                .ToList();

            var employeeProjections = new List<EmployeeProjectionDto>();

            foreach (var e in employees)
            {
                var empProjection = mapper.Map<EmployeeProjectionDto>(e);
                employeeProjections.Add(empProjection);
            }

            return employeeProjections;
        }
    }
}