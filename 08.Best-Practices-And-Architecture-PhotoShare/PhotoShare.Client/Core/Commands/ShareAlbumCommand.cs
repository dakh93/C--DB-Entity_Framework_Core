namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Messages;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data)
        {
            int albumId;
            bool isNumeric = int.TryParse(data[0], out albumId);
            var username = data[1];
            Role permission;
            string formatPermissionWord = Char.ToUpper(data[2][0]) + data[2].Substring(1);
            bool validPermission = Enum.TryParse(formatPermissionWord, out permission);

            if (!isNumeric)
            {
                throw new ArgumentException(ErrorMessages.AlbumIdShouldBeNumber);
            }

            using (var context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                   .Where(u => u.isLogged == true)
                   .FirstOrDefault();

                if (loggedUser == null || loggedUser.Username != username)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }

                var album = context.Albums
                    .Where(a => a.Id == albumId)
                    .FirstOrDefault();

                if(album == null)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.AlbumNotFound, albumId));
                }


                if (loggedUser == null)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.UserNotFound, username));
                }

                //var validPermissions = new string[] { "owner", "viewer" };

               // var isPermissionValid = validPermissions.Contains(permission.ToLower());

                if (!validPermission)
                {
                    throw new ArgumentException(ErrorMessages.InvalidPermission);
                }

                

                var albumRole = new AlbumRole()
                {
                    Album = album,
                    AlbumId = albumId,
                    Role = permission,
                    User = loggedUser,
                    UserId = loggedUser.Id,
                };

                loggedUser.AlbumRoles.Add(albumRole);
                album.AlbumRoles.Add(albumRole);
                
                context.SaveChanges();

                var result = string.Format(SuccessMessages.AddUserToAlbum, loggedUser.Username, album.Name, permission);

                return result;
            }
        }
    }
}
