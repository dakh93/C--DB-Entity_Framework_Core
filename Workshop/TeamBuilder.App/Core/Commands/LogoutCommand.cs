
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForLoggedUserLogout();
            
            var username = userService.Logout();

            var message = String.Format(SuccessMessages.SuccessfullyLogout, username);

            return message;
        }
    }
}
