// See https://aka.ms/new-console-template for more information


using P02_Database_First.Data;

var dbContext = new SoftUniContext();

var employees = dbContext.Employees
    .Select(e => new
    {
        e.EmployeeId,
        e.FirstName,
        e.LastName,
        e.MiddleName,
        e.JobTitle,
        e.Salary,
    })
    .OrderBy(e => e.EmployeeId);


foreach (var emp in employees)
{
    Console.WriteLine(
        String.Join(" ", emp.FirstName,
                         emp.LastName,
                         emp.MiddleName,
                         emp.JobTitle,
                         emp.Salary.ToString("f2")));
}