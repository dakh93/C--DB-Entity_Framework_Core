using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForLoggedUserRegister();

            var username = args[0];
            var password = args[1];

            Validator.CheckIfUserExists(username);
            Validator.CheckIfUserDeleted(username);
            Validator.CheckIfPasswordCorrect(username, password);

            this.userService.Login(username, password);

            var message = String.Format(SuccessMessages.SuccessfullLogin, username);

            return message;
        }
    }
}
