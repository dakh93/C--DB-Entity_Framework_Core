
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class AddTeamToCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_ELEMENTS = 2;

        public AddTeamToCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_ELEMENTS);

            var eventName= args[0];
            var teamName = args[1];

            Validator.CheckIfEventExists(eventName);
            Validator.CheckForLoggedUserLogout();
            Validator.CheckIfTeamExistsWhenDisband(teamName);
            Validator.CheckIfLoggedUserIsCreatorOfEvent(eventName);
            Validator.CheckIfTeamIsAlreadyAdddedToEvent(eventName, teamName);

            userService.AddTeamTo(eventName, teamName);

            var message = String.Format(SuccessMessages.SuccessfullyAddedTeamToEvent, teamName, eventName);

            return message;
        }
    }
}
