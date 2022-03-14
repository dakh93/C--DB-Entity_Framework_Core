
namespace TeamBuilder.App.Core.Contracts
{
    public interface ICommandParser
    {
        ICommand ParseCommand(IServiceProvider serviceProvider, string commandDetails);
    }
}
