namespace PhotoShare.Client.Core.Commands
{
    using System.Linq;

    using Models;
    using Data;
    using PhotoShare.Client.Core.Messages;
    using System;
    using Microsoft.EntityFrameworkCore;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                   .Any(u => u.isLogged == true);

                if (!loggedUser)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }

                bool isTownExists = context.Towns
                    .AsNoTracking()
                    .Any(t => t.Name == townName);

                if (isTownExists)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.ExistingTown, townName));
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return string.Format(SuccessMessages.AddedTownToDatabase, town.Name);
            }
        }
    }
}
