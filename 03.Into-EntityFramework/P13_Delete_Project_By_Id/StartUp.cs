
using P02_Database_First.Data;

using (var dbContext = new SoftUniContext())
{
    var projects = dbContext.EmployeesProjects.Where(x => x.ProjectId == 2);
    dbContext.EmployeesProjects.RemoveRange(projects);

    var project = dbContext.Projects.Find(2);
    dbContext.Projects.Remove(project);

    dbContext.SaveChanges();

    var result = dbContext.Projects.Take(10);
    foreach (var p in result)
    {
        Console.WriteLine(p.Name);
    }
}