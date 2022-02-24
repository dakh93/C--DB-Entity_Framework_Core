using PhotoShare.Client.Core.Messages;
using PhotoShare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand
    {
        public static string Execute()
        {
            using (var context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                    .Where(u => u.isLogged == true)
                    .FirstOrDefault();

                if (loggedUser == null)
                {
                    throw new InvalidOperationException(ErrorMessages.NoLoggedUser);
                }

                loggedUser.isLogged = false;

                context.SaveChanges();

                var result = string.Format(SuccessMessages.LogoutSuccess, loggedUser.Username);

                return result;

            }
        }
    }
}
