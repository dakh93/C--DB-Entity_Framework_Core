
using P02_Database_First.Data;

var dbContext = new SoftUniContext();

using (dbContext)
{
    var employees = dbContext.Employees
        .Where(e => e.Projects.All(
            p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003))
        .Select(e => new
        {
            Name = $"{e.FirstName} {e.LastName} - Manager: {dbContext.Employees.Where(m => m.EmployeeId == e.ManagerId).Select(t => t.FirstName + " " + t.LastName).FirstOrDefault()}",
            Projects = e.Projects
        })
        .Take(30);

    foreach (var e in employees)
    {
        Console.WriteLine($"{e.Name}");
        foreach (var p in e.Projects)
        {
            var format = "M/d/yyyy h:mm:ss tt";
            var notFinished = "not finished";
            var endDate = p.EndDate != null ? p.EndDate?.ToString(format) : notFinished;
            Console.WriteLine($"--{p.Name} - {p.StartDate.ToString(format)} - {endDate} ");
        }
    }
}