namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Client.Core.Messages;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data)
        {
            var username = data[0];
            var albumTitle = data[1];
            Color bgColor;
            string formatColorWord = Char.ToUpper(data[2][0]) + data[2].Substring(1);
            bool validPermission = Enum.TryParse(formatColorWord, out bgColor);


            using (var context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                   .Where(u => u.isLogged == true)
                   .Include(u => u.AlbumRoles)
                   .ThenInclude(ar => ar.Album)
                   .FirstOrDefault();

                if (loggedUser == null || loggedUser.Username != username)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }


                //Album does exists
                var album = context.Albums
                    .Where(a => a.Name == albumTitle)
                    .FirstOrDefault();

                if (album != null)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.AlbumAlreadyExists, albumTitle));
                }

                //Background color does not exists
               

                if (!validPermission)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.ColorNotFound, bgColor));
                }

                var listOfAlbumTags = new List<AlbumTag>();
                //If any tags is not found in Database
                album = new Album()
                {
                    Name = albumTitle,
                };

                for (int i = 3; i < data.Length; i++)
                {
                    var tagName = data[i];

                    var currentTag = context.Tags
                        .Where(t => t.Name == tagName)
                        .FirstOrDefault();

                    if (currentTag == null)
                    {
                        throw new ArgumentException(string.Format(ErrorMessages.InvalidTags, tagName));
                    }


                    AlbumTag currAlbumTag = new AlbumTag()
                    {
                        Album = album,
                        AlbumId = album.Id,
                        Tag = currentTag,
                        TagId = currentTag.Id
                    };
                    listOfAlbumTags.Add(currAlbumTag);
                    currentTag.AlbumTags.Add(currAlbumTag);
                    context.AlbumTags.Add(currAlbumTag);
                }

                var albumRole = new AlbumRole()
                {
                    Album = album,
                    AlbumId = album.Id,
                    Role = Role.Owner,
                    User = loggedUser,
                    UserId = loggedUser.Id,
                };


                
                album.AlbumRoles.Add(albumRole);
                album.BackgroundColor = bgColor;
                album.AlbumTags = listOfAlbumTags;

                context.SaveChanges();

            }

            var result = string.Format(SuccessMessages.CreatedAlbum, albumTitle);

            return result;
        }
    }
}
