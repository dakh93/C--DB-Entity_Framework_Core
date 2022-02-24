namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Models;
    using Data;
    using PhotoShare.Client.Core.Messages;
    using Microsoft.EntityFrameworkCore;

    public class RegisterUserCommand
    {
        // RegisterUser <username> <password> <repeat-password> <email>
        public static string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            if (password != repeatPassword)
            {
                throw new ArgumentException(ErrorMessages.PasswordDontMatch);
            }


            using (PhotoShareContext context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                   .Any(u => u.isLogged == true);

                if (loggedUser)
                {
                    throw new InvalidOperationException(ErrorMessages.NeedToLogout);
                }

                bool isUserExists = context.Users
                    .AsNoTracking()
                    .Any(x => x.Username == username);

                if (isUserExists)
                {
                    throw new InvalidOperationException(string.Format(ErrorMessages.UsernameIsTaken, username));
                }

                User user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    IsDeleted = false,
                    RegisteredOn = DateTime.Now,
                    LastTimeLoggedIn = DateTime.Now
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return string.Format(SuccessMessages.RegisteredUser, username);

        }
    }
}
