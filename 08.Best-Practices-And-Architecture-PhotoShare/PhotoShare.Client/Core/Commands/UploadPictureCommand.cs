namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Messages;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            var albumName = data[0];
            var pictureTitle = data[1];
            var pictureFilePath = data[2];

            using (var context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                   .Where(u => u.isLogged == true)
                   .FirstOrDefault();

                if (loggedUser == null)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }

                var album = context.Albums
                .Where(a => a.Name == albumName)
                .Include(a => a.Pictures)
                .FirstOrDefault();

                if (album == null)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.AlbumNotFound, albumName));
                }
                var getUserOwner = context.Albums
                    .Where(a => a.Name == albumName)
                    .Include(a => a.AlbumRoles)
                    .ThenInclude(x => x.User)
                    .Select(a => a.AlbumRoles.Where(r => r.Role == Role.Owner))
                    .FirstOrDefault()
                    .FirstOrDefault();

                if (getUserOwner.User.Id != loggedUser.Id)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }

                var picture = new Picture()
                {
                    Album = album,
                    AlbumId = album.Id,
                    Path = pictureFilePath,
                    Title = pictureTitle,
                    UserProfile = getUserOwner.User,
                    UserProfileId = getUserOwner.UserId,
                };
                if (album.Pictures.Any(p => p.Title == picture.Title))
                {
                    throw new ArgumentException(string.Format(ErrorMessages.ExistingPicture, picture.Title));
                }

                album.Pictures.Add(picture);
                context.SaveChanges();

                var result = string.Format(SuccessMessages.UploadedPictureToAlbum, picture.Title, album.Name);

                return result;
            }
        }
    }
}
