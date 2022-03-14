
using System.Text;
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowEventCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_LENGTH = 1;

        public ShowEventCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_LENGTH);

            var eventName = args[0];

            Validator.CheckIfEventExists(eventName);

            var eventInfo = userService.ShowEvent(eventName);

            return eventInfo;

        }
    }
}
