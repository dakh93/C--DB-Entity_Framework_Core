
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class InviteToTeamCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_LENGTH = 2;

        public InviteToTeamCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_LENGTH);

            var teamName = args[0];
            var username = args[1];

            Validator.CheckForLoggedUserLogout();
            Validator.CheckIfUserExists(username);
            Validator.CheckIfTeamExists(teamName);

            var user = Database.GetUserByName(username);
            var team = Database.GetTeamByName(teamName);
            Validator.CheckIfLoggedUserIsCreatorOrPartOfTeam(teamName, user);
            Validator.CheckForActiveTeamInvite(user, team);

            userService.InviteToTeam(team, user);

            var message = String.Format(SuccessMessages.SuccessfullyInvitedToTeam, teamName, username);

            return message;

        }
    }
}
