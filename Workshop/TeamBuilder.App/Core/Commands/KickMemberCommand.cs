
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class KickMemberCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_LENGTH = 2;

        public KickMemberCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_LENGTH);
            var teamName = args[0];
            var usernameToKick = args[1];
            Validator.CheckForLoggedUserLogout();
            Validator.CheckIfUserExists(usernameToKick);
            Validator.CheckIfTeamExists(teamName);

            var userToKick = Database.GetUserByName(usernameToKick);
            var loggedUser = Database.GetLoggedUser();
            var team = Database.GetTeamByName(teamName);

            Validator.CheckIfUserIsPartOfTeam(userToKick, team);
            Validator.CheckIfLoggedUserIsTeamCreator(loggedUser, team);
            Validator.CheckIfUserToKickIsCreator(usernameToKick, team);

            userService.KickMember(teamName, usernameToKick);

            var message = String.Format(SuccessMessages.SuccessfullyKickedMember, usernameToKick, teamName);

            return message;

        }
    }
}
