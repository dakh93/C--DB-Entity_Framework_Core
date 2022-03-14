
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowTeamCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_LENGTH = 1;

        public ShowTeamCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_LENGTH);

            var teamName = args[0];

            Validator.CheckIfTeamExistsWhenDisband(teamName);

            var teamInfo = userService.ShowTeam(teamName);

            return teamInfo;
        }
    }
}
