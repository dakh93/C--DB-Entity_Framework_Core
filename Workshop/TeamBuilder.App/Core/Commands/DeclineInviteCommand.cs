
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class DeclineInviteCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_LENGTH = 1;

        public DeclineInviteCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_LENGTH);

            var teamName = args[0];
            var user = Database.GetLoggedUser();

            Validator.CheckForLoggedUserLogout();
            Validator.CheckIfTeamExists(teamName);
            var team = Database.GetTeamByName(teamName);
            Validator.CheckForInviteFromTeam(user, team);

            userService.DeclineInvite(teamName);

            var message = String.Format(SuccessMessages.SuccessfullyDeclinedInviteToTeam, teamName);

            return message;
        }
    }
}
