namespace Employees.App
{

    using Microsoft.Extensions.DependencyInjection;

    using Employees.Data;
    using Microsoft.EntityFrameworkCore;
    using Employees.Services;

    class StartUp
    {

        static void Main()
        {
            var serviceProvider = ConfigureServices();

            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<EmployeeService>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<AutomapperProfile>());
            

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}