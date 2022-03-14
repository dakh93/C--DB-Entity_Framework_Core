
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class DisbandCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_ELEMENTS = 1;

        public DisbandCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_ELEMENTS);
            Validator.CheckForLoggedUserLogout();

            var teamName = args[0];

            Validator.CheckIfTeamExistsWhenDisband(String.Format(teamName, teamName));

            var loggedUser = Database.GetLoggedUser();
            var team = Database.GetTeamByName(teamName);

            Validator.CheckIfLoggedUserIsTeamCreator(loggedUser, team);
            userService.Disband(teamName);

            var message = String.Format(SuccessMessages.SuccessfullyDisbandTeam, teamName);

            return message;
        }
    }
}
