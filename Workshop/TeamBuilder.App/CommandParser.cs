
using System.Reflection;
using TeamBuilder.App.Core.Contracts;

namespace TeamBuilder.App
{
    public class CommandParser : ICommandParser
    {
        private const string SUFFIX = "Command";

        public ICommand ParseCommand(IServiceProvider serviceProvider, string commandDetails)
        {
            var details = commandDetails.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var inputCommand = details[0] + SUFFIX;

            var arguments = details.Skip(1).ToArray();

            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)));

            var commandType = commandTypes
                .SingleOrDefault(c => c.Name.ToLower() == inputCommand.ToLower());

            if (commandType == null)
            {
                throw new NotSupportedException(String.Format(ErrorMessages.InvalidCommand, inputCommand));
            }

            var constructor = commandType
                .GetConstructors()
                .FirstOrDefault();

            var constructorParams = constructor
                .GetParameters()
                .Select(p => p.ParameterType);

            var constructorArgs = constructorParams
                .Select(c => serviceProvider.GetService(c))
                .ToArray();

            var command = (ICommand)constructor.Invoke(constructorArgs);

            return command;
        }

        
    }
}
