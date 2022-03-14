
using Microsoft.Extensions.DependencyInjection;
using TeamBuilder.App.Core.Contracts;

namespace TeamBuilder.App.Core
{
    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ICommandParser commandParser;

        private readonly string ExitCommand = "exit";

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.commandParser = this.serviceProvider.GetService<ICommandParser>();

        }
        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Command: ");
                    var input = Console.ReadLine();

                    //TERMINATE PROGRAM
                    if (input.ToLower() == ExitCommand )
                    {
                        Environment.Exit(0);
                    }

                    string[] commandTokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    var commandName = commandTokens[0];

                    string[] commandArgs = commandTokens.Skip(1).ToArray();

                    var command = this.commandParser.ParseCommand(serviceProvider, input);

                    var result = command.Execute(commandArgs);

                    Console.WriteLine(result);


                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
