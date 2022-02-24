namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;
    using PhotoShare.Client.Core.Messages;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            var username = data[0];
            var property = data[1].ToLower();
            var newValue = data[2];

            var validProperties = new string[] { "password", "borntown", "currenttown" };

            using (var context = new PhotoShareContext())
            {
                var user = context.Users
                    .Where(x => x.Username == username)
                    .FirstOrDefault();

                //Invalid credentials
                if (user.isLogged == false)
                {
                    throw new ArgumentException(ErrorMessages.InvalidCredentials);
                }

                //User does not exist
                if (user == null)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.UserNotFound, username));
                }

                //Property not found
                if (!validProperties.Contains(property))
                {
                    throw new ArgumentException(string.Format(ErrorMessages.InvalidProperty, property));
                }

                switch (property)
                {
                    case "password":
                        if (!newValue.Any(v => Char.IsLower(v)) || 
                            !newValue.Any(v => Char.IsDigit(v)))
                        {
                            throw new ArgumentException(string.Format(ErrorMessages.InvalidPasswordValue, newValue));
                        }

                        user.Password = newValue;
                        break;

                    case "borntown":
                        var bornTown = context.Towns
                            .Where(t => t.Name == newValue)
                            .FirstOrDefault();

                        if (bornTown == null);
                        {
                            throw new ArgumentException(string.Format(ErrorMessages.InvalidTownValue, newValue));
                        }
                        
                        user.BornTown = bornTown;
                        break;

                    case "currenttown":
                        var currentTown = context.Towns
                            .Where(t => t.Name == newValue)
                            .FirstOrDefault();

                        if (currentTown == null) ;
                        {
                            throw new ArgumentException(string.Format(ErrorMessages.InvalidTownValue, newValue));
                        }

                        user.CurrentTown = currentTown;
                        break;

                }
                context.SaveChanges();

                var result = string.Format(SuccessMessages.ModifiedUser, username, property, newValue);
                
                return result;
            }
        }

        
    }
}
