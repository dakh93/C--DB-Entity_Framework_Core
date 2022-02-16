
using P02_Database_First.Data;

var dbContext = new SoftUniContext();

using (dbContext)
{
    var result = dbContext.Addresses
        .Select(a => new
        {
            AddressText = a.AddressText,
            TownName = dbContext.Towns.FirstOrDefault(t => t.TownId == a.TownId).Name,
            EmployeesCount = a.Employees.Count()
        })
        .OrderByDescending(a => a.EmployeesCount)
        .ThenBy(a => a.TownName)
        .ThenBy(a => a.AddressText)
        .Take(10);

    foreach (var address in result)
    {
        Console.WriteLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
    }
}