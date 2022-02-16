
using P02_Database_First.Data;

using (var dbContext = new SoftUniContext())
{

    
    var projects = dbContext.Projects
                            .Select(p => new
                            {
                                Name = p.Name,
                                Description = p.Description,
                                StartDate = p.StartDate,
                            })
                            .OrderByDescending(p => p.StartDate)
                            .ThenBy(p => p.StartDate);

    foreach (var project in projects)
    {
        Console.WriteLine(project.Name);
        Console.WriteLine(project.Description);
        Console.WriteLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt"));
    }
}