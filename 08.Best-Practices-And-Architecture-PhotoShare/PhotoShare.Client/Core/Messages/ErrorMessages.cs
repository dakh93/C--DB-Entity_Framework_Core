using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Client.Core.Messages
{
    internal class ErrorMessages
    {
        public const string PasswordDontMatch = "Password do not match!";

        public const string UsernameIsTaken = "Username {0} is already taken!";

        public const string UserNotFound = "User {0} not found!";

        public const string InvalidProperty = "Property {0} not supported!";

        public const string ExistingTown = "Town {0} was already added!";

        public const string InvalidCommand = "Command {0} not valid!";

        public const string InvalidPasswordValue = "Value {0} not valid." + "\n\r" + "Invalid Password!";
        public const string InvalidTownValue = "Value {0} not valid." + "\n\r" + "Invalid Town!";

        public const string AlreadyDeletedUser = "User {0} is already deleted!";

        public const string TagAlreadyExists = "Tag {0} exists!";

        public const string AlbumAlreadyExists = "Album {0} exists!";

        public const string ColorNotFound = "Color {0} not found!";

        public const string InvalidTags = "Invalid tags!";

        public const string AlbumOrTagDoesntExists = "Either tag or album do not exist!";

        public const string UsersAreAlreadyFriends = "{0} is already a friend to {1}";

        public const string FriendRequestAlreadySent = "Friend request already sent!";

        public const string FriendRequestAlreadyReceived = "{0} already received a friend request from {1}";

        public const string NotAddedFriend = "{0} has not added {1} as a friend";

        public const string NoFriends = "No friends for this user. :(";

        public const string AlbumIdShouldBeNumber = "Album ID should be number!";

        public const string AlbumNotFound = "Album {0} not found!";

        public const string InvalidPermission = "Permission must be either \"Owner\" or \"Viewer\"!";

        public const string ExistingPicture = "Picture {0} already exists!";

        public const string InvalidUsernameOrPassword = "Invalid username or password";

        public const string LoggedUser = "You should logout first!";

        public const string NoLoggedUser = "You should log in first in order to logout.";

        public const string InvalidCredentials = "Invalid credentials!";

        public const string NeedToLogout = "You need first to logout in order to do this command!";
    }
}
