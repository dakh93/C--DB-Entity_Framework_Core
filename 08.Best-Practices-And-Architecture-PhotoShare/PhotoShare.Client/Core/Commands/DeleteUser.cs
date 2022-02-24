namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;
    using PhotoShare.Client.Core.Messages;

    public class DeleteUser
    {
        // DeleteUser <username>
        public static string Execute(string[] data)
        {
            string username = data[0];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username && u.isLogged == true);

                if (user == null)
                {
                    throw new InvalidOperationException(string.Format(ErrorMessages.UserNotFound, username));
                }

                // TODO: Delete User by username (only mark him as inactive)
                if (user.IsDeleted == true)
                {
                    throw new InvalidOperationException(string.Format(ErrorMessages.AlreadyDeletedUser, username));
                }

                user.IsDeleted = true;
                user.isLogged = false;
                context.SaveChanges();

                var result = string.Format(SuccessMessages.DeletedUser, username);
                return result;
            }
        }
    }
}
