// See https://aka.ms/new-console-template for more information

using System.Linq;

using P02_Database_First.Data;

var dbContext = new SoftUniContext();

using (dbContext)
{
    var minSalary = 50000;

    var employeesNames = dbContext.Employees
        .Where(e => e.Salary > minSalary)
        .OrderBy(e => e.FirstName)
        .Select(e => e.FirstName);

    foreach (var emp in employeesNames)
    {
        Console.WriteLine(emp);
    }
}
