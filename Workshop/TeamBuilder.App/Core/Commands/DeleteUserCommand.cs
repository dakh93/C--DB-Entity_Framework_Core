
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Data;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userService;

        public DeleteUserCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForLoggedUserLogout();

            string username = userService.DeleteUser();

            var message = String.Format(SuccessMessages.SuccessfullyDeletedUser, username);

            return message;
        }
    }
}
