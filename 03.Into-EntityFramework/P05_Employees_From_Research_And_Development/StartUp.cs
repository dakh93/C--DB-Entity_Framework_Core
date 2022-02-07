
using P02_Database_First.Data;

var dbContext = new SoftUniContext();

using (dbContext)
{
    var employees = dbContext.Employees
        .Where(e => e.Department.Name == "Research and Development")
        .OrderBy(e => e.Salary)
        .ThenByDescending(e => e.FirstName)
        .Select(e => new
        {
            e.FirstName,
            e.LastName,
            DepartmentName = e.Department.Name,
            e.Salary
        });

    foreach (var emp in employees)
    {
        Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.DepartmentName} - ${emp.Salary:f2}");
    }
}