namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Client.Core.Messages;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var user1 = data[0];
            var user2 = data[1];

            using (var context = new PhotoShareContext())
            {
                var requestSender = context.Users
                    .Include(a => a.FriendsAdded)
                        .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == user1);

                if (requestSender.isLogged == false)
                {
                    throw new ArgumentException(ErrorMessages.InvalidCredentials);
                }

                if (requestSender == null)
                {
                    throw new ArgumentException(ErrorMessages.UserNotFound, user1);
                }

                var requestReceiver = context.Users
                    .Include(a => a.FriendsAdded)
                        .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == user2);

                if (requestReceiver == null)
                {
                    throw new ArgumentException(ErrorMessages.UserNotFound, user2);
                }

                bool alreadyAdded = requestSender.FriendsAdded.Any(f => f.Friend == requestReceiver);

                bool isAccepted = requestReceiver.FriendsAdded.Any(f => f.Friend == requestSender);

                //They are already friends
                if (alreadyAdded && isAccepted)
                {
                    throw new InvalidOperationException(String.Format(ErrorMessages.UsersAreAlreadyFriends,requestReceiver.Username, requestSender.Username));
                }
                //Request is not accepted
                if (alreadyAdded && !isAccepted)
                {
                    throw new InvalidOperationException(String.Format(ErrorMessages.FriendRequestAlreadySent));
                }

                if (!alreadyAdded && isAccepted)
                {
                    throw new InvalidOperationException(String.Format(ErrorMessages.FriendRequestAlreadyReceived,requestReceiver.Username, requestSender.Username));
                }

                var friendship = new Friendship()
                {
                    User = requestSender,
                    UserId = requestSender.Id,
                    Friend = requestReceiver,
                    FriendId = requestReceiver.Id,
                };

              
                requestSender.FriendsAdded.Add(friendship);

                context.SaveChanges();

                var resultMessage = string.Format(SuccessMessages.AddFriendSuccess, requestReceiver.Username, requestSender.Username);

                return resultMessage; 
            }

        }
    }
}
