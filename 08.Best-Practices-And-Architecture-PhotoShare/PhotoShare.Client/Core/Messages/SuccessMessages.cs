using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Client.Core.Messages
{
    internal class SuccessMessages
    {
        public const string RegisteredUser = "User {0} was registered successfully!";

        public const string DeletedUser = "User {0} was deleted successfully!";

        public const string AddedTownToDatabase = "{0} was added to database!";

        public const string ModifiedUser = "User {0} {1} is {2}.";

        public const string AddedTag = "Tag {0} was added successfully!";

        public const string CreatedAlbum = "Album {0} successfully created!";

        public const string AddedTagToAlbum = "Tag {0} added to {1}";

        public const string AddFriendSuccess = "Friend {0} added to {1}";

        public const string AddUserToAlbum = "Username {0} added to album {1} ({2})";

        public const string UploadedPictureToAlbum = "Picture {0} added to {1}!";

        public const string LoginSuccess = "User {0} successfully logged in!";

        public const string LogoutSuccess = "User {0} successfully logged out!";


    }
}
