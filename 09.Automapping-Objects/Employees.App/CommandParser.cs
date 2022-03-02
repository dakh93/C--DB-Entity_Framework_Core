using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using Employees.App.Command.Contracts;


namespace Employees.App
{
    internal class CommandParser
    {
        public static ICommand Parse(IServiceProvider serviceProvider, string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)));

            var commandType = commandTypes
                .SingleOrDefault(c => c.Name.ToLower() == $"{commandName.ToLower()}command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command.");
            }

            var constructor = commandType
                .GetConstructors()
                .FirstOrDefault();

            var constructorParams = constructor
                .GetParameters()
                .Select(pi => pi.ParameterType);

            var constructorArgs = constructorParams
                .Select(p => serviceProvider.GetService(p))
                .ToArray();

            var command = (ICommand)constructor.Invoke(constructorArgs);

            return command; 
        }
    }
}
