using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App;
using TeamBuilder.App.Core;
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Data;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

var serviceProvider = ConfigureServices();

var engine = new Engine(serviceProvider);

engine.Run();

static IServiceProvider ConfigureServices()
{
    var serviceCollection = new ServiceCollection();

    serviceCollection.AddDbContext<TeamBuilderContext>(options =>
           options.UseSqlServer(Configuration.ConnectionString));

    serviceCollection.AddTransient<ICommandParser, CommandParser>();
    serviceCollection.AddTransient<IUserService, UserService>();
    

    var serviceProvider = serviceCollection.BuildServiceProvider();

    return serviceProvider;
}