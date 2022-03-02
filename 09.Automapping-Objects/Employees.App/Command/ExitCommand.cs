
using Employees.App.Command.Contracts;

namespace Employees.App.Command
{
    internal class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);

            return string.Empty;
        }
    }
}
