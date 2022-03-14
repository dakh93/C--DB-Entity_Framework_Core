using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamBuilder.App.Core.Contracts;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private const int EXPECTED_ELEMENTS = 7;
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }
        public string Execute(string[] args)
        {
            Validator.CheckForArgumentCount(args, EXPECTED_ELEMENTS);

            var username = args[0];
            var password= args[1];
            var repetedPassword = args[2];
            var firstName = args[3];
            var lastName = args[4];
            var age = Validator.CheckAge(args[5]);
            var gender = Validator.CheckGender(args[6]);

            Validator.CheckUsername(username);
            Validator.CheckPasswordFormat(password, repetedPassword);
            Validator.CheckForLoggedUserRegister();

            this.userService.RegisterUser(username, password, repetedPassword, firstName, lastName, age, gender);

            var message = String.Format(SuccessMessages.SuccessfullRegistration, username);

            return message;
        }

        
    }
}
