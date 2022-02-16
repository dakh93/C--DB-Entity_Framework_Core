
using P02_Database_First.Data;

var dbContext = new SoftUniContext();

using (dbContext)
{
    var result = dbContext
        .Employees
        .Where(e => e.EmployeeId == 147);

    foreach (var employee in result)
    {
        Console.WriteLine($"{employee.FirstName} {employee.LastName}");

        foreach (var project in employee.Projects.OrderBy(p => p.Name))
        {
            Console.WriteLine(project);
        }
    }
        
}