
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateTeamCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly int EXPECTED_ELEMENTS_MIN = 2;
        private readonly int EXPECTED_ELEMENTS_MAX = 3;

        public CreateTeamCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_ELEMENTS_MIN, EXPECTED_ELEMENTS_MAX);
            Validator.CheckForLoggedUserLogout();

            var teamName = args[0];
            var teamAcronym = args[1];
            string description = String.Empty;
            if (args.Length == EXPECTED_ELEMENTS_MAX)
            {
                description = args[2];
            }

            Validator.CheckIfTeamExistsWhenInvite(teamName);
            Validator.CheckAcronymLength(teamAcronym);

            var loggedUser = Database.GetLoggedUser();

            userService.CreateTeam(teamName, teamAcronym, description, loggedUser);

            var message = String.Format(SuccessMessages.SuccessfullyCreatedTeam, teamName);

            return message;

        }
    }
}
