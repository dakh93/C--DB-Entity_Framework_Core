namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using Commands;
    using PhotoShare.Client.Core.Messages;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var command = commandParameters[0].ToLower();
            var commandParams = commandParameters
                .Skip(1)
                .ToArray();

            string result;
            
            switch (command)
            {
                case "registeruser":
                    result = RegisterUserCommand.Execute(commandParams);
                    break;

                case "addtown":
                    result = AddTownCommand.Execute(commandParams);
                    break;

                case "modifyuser":
                    result = ModifyUserCommand.Execute(commandParams);
                    break;

                case "deleteuser":
                    result = DeleteUser.Execute(commandParams);
                    break;

                case "addtag":
                    result = AddTagCommand.Execute(commandParams);
                    break;

                case "createalbum":
                    result = CreateAlbumCommand.Execute(commandParams);
                    break;

                case "addtagto":
                    result = AddTagToCommand.Execute(commandParams);
                    break;

                case "addfriend":
                    result = AddFriendCommand.Execute(commandParams);
                    break;

                case "acceptfriend":
                    result = AcceptFriendCommand.Execute(commandParams);
                    break;

                case "listfriends":
                    result = PrintFriendsListCommand.Execute(commandParams);
                    break;

                case "sharealbum":
                    result = ShareAlbumCommand.Execute(commandParams);
                    break;

                case "uploadpicture":
                    result = UploadPictureCommand.Execute(commandParams);
                    break;

                case "login":
                    result = LoginCommand.Execute(commandParams);
                    break;

                case "logout":
                    result = LogoutCommand.Execute();
                    break;

                case "exit":
                    result = ExitCommand.Execute();
                    break;

                default:
                    throw new InvalidOperationException(string.Format(ErrorMessages.InvalidCommand, command));
            }
            return result;
        }
    }
}
