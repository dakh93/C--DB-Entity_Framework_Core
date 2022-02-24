namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using Utilities;
    using PhotoShare.Client.Core.Messages;
    using System.Linq;
    using System;

    public class AddTagCommand
    {
        // AddTag <tag>
        public static string Execute(string[] data)
        {
            string tag = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                    .Where(u => u.isLogged == true)
                    .FirstOrDefault();

                if (loggedUser == null)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }

                var isTagExists = context.Tags
                    .Any(t => t.Name == tag);

                if (isTagExists)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.TagAlreadyExists));
                }

                context.Tags.Add(new Tag
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            var result = string.Format(SuccessMessages.AddedTag, tag);
            return result;
        }
    }
}
