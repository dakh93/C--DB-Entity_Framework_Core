namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Client.Core.Messages;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            var albumName = data[0];
            var tagName = data[1];

            using (var context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                   .Where(u => u.isLogged == true)
                   .Include(u => u.AlbumRoles)
                   .ThenInclude(ar => ar.Album)
                   .FirstOrDefault();

                var isOwnerOfThisAlbum = loggedUser.AlbumRoles
                    .Where(ar => ar.Album.Name == albumName && ar.Role == Role.Owner)
                    .FirstOrDefault();

                if (loggedUser == null || isOwnerOfThisAlbum == null)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }

                var tag = context.Tags
                    .Where(t => t.Name == tagName)
                    .FirstOrDefault();

                if (tag == null)
                {
                    throw new ArgumentException(ErrorMessages.AlbumOrTagDoesntExists);
                }

                var album = context.Albums
                    .Where(t => t.Name == albumName)
                    .FirstOrDefault();

                if (album == null)
                {
                    throw new ArgumentException(ErrorMessages.AlbumOrTagDoesntExists);
                }

                var albumTag = new AlbumTag()
                {
                    Album = album,
                    AlbumId = album.Id,
                    Tag = tag,
                    TagId = tag.Id
                };

                album.AlbumTags.Add(albumTag);
                tag.AlbumTags.Add(albumTag);
                context.AlbumTags.Add(albumTag);

                context.SaveChanges();
            }
            
            var result = string.Format(SuccessMessages.AddedTagToAlbum,tagName,albumName);
            return result;
        }
    }
}
