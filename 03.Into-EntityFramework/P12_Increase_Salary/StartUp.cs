using P02_Database_First.Data;
using P02_Database_First.Data.Models;

using (var dbContext = new SoftUniContext())
{
    var increased = dbContext.Employees
                             .Where(e => e.DepartmentId == 1 ||
                                         e.DepartmentId == 2 ||
                                         e.DepartmentId == 4 ||
                                         e.DepartmentId == 11)
                             .Select(e => new Employee()
                             {
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 Salary = e.Salary
                             })
                             .OrderBy(e => e.FirstName)
                             .ThenBy(e => e.LastName);

    foreach (var e in increased)
    {
        e.Salary *= 1.12m;
        Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:F2})");
    }
    dbContext.SaveChanges();
}