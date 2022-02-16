
using P02_Database_First.Data;
using P02_Database_First.Data.Models;

var dbContext = new SoftUniContext();

using (dbContext)
{ 

    var address = new Address()
    {
        AddressText = "Vitoshka 15",
        TownId = 4,
        Employees = new List<Employee>()
        {
            dbContext.Employees.FirstOrDefault(e => e.LastName == "Nakov")
        }
    };
    dbContext.Addresses.Add(address);

    dbContext.SaveChanges();

    Console.WriteLine(
        String.Join(Environment.NewLine,
        dbContext
        .Employees
        .OrderByDescending(e => e.AddressId)
        .Take(10)
        .Select(e => e.Address.AddressText)
        ));
}
