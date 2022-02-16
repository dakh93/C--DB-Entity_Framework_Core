
using P02_Database_First.Data;

using (var dbContext  = new SoftUniContext())
{
    var employees = dbContext.Employees
        .Where(e => e.FirstName.StartsWith("Sa"))
        .OrderBy(e => e.FirstName)
        .ThenBy(e => e.LastName);

    foreach (var e in employees)
    {
        Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})");
    }
}