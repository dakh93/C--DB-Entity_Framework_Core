
using P02_Database_First.Data;

var dbContext = new SoftUniContext();

using (dbContext)
{
    var departments = dbContext.Departments
        .Where(d => d.Employees.Count > 5)
        .OrderBy(d => d.Employees.Count)
        .ThenBy(d => d.Name)
        .Select(d => new
        {
            DepartmentName = d.Name,
            ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
            Employees = d.Employees.Select(e => new
            {
                e.FirstName,
                e.LastName,
                e.JobTitle
            })
            .OrderBy(d => d.FirstName)
            .ThenBy(d => d.LastName)
        })
        .ToArray();

    var emp = dbContext.Employees;

    foreach (var dep in departments)
    {
        Console.WriteLine($"{dep.DepartmentName} - {dep.ManagerName}");
        foreach (var employee in dep.Employees)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
        }
        Console.WriteLine(new string('-',10));
    }
}