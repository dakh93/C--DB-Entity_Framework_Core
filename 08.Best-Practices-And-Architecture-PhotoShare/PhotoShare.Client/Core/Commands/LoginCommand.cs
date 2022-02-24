
using PhotoShare.Client.Core.Messages;
using PhotoShare.Data;
using PhotoShare.Models;
using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand
    {
        public static string Execute(string[] data)
        {
            var username = data[0];
            var password = data[1];

            using (var context = new PhotoShareContext())
            {
                var anyUserLogged = CheckForLoggedUser(context);

                if (anyUserLogged)
                {
                    throw new ArgumentException(ErrorMessages.LoggedUser);
                }

                var validUsername = CheckForUsername(context, username);

                if (!validUsername)
                {
                    throw new ArgumentException(ErrorMessages.InvalidUsernameOrPassword);
                }

                var validPasswordForUsername = CheckForUsernamePassword(context, username, password);

                if (!validPasswordForUsername)
                {
                    throw new ArgumentException(ErrorMessages.InvalidUsernameOrPassword);
                }

                var user = context.Users
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefault();

                user.isLogged = true;

                context.SaveChanges();

                var result = string.Format(SuccessMessages.LoginSuccess, username);

                return result;
            }

        }

        private static bool CheckForUsernamePassword(PhotoShareContext context, string username, string password)
        {
            var user = context.Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            if (user.Password == password) return true;
            return false;
        }

        private static bool CheckForUsername(PhotoShareContext context, string username)
        {
            var isUsernameValid = context.Users
                .Any(u => u.Username == username);

            return isUsernameValid;
        }

        private static bool CheckForLoggedUser(PhotoShareContext context)
        {
            var loggedUser = context.Users
                .Any(u => u.isLogged == true);

            return loggedUser;
        }
    }
}
