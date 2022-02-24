namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Client.Core.Messages;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var user1 = data[0];
            var user2 = data[1];

            using (var context = new PhotoShareContext())
            {
                var loggedUser = context.Users
                   .Where(u => u.isLogged == true)
                   .FirstOrDefault();

                if (loggedUser == null || loggedUser.Username != user1)
                {
                    throw new InvalidOperationException(ErrorMessages.InvalidCredentials);
                }

                var receiverUser = context.Users
                    .Include(a => a.FriendsAdded)
                        .ThenInclude(fa => fa.Friend)
                    .Where(u => u.Username == user1)
                    .FirstOrDefault();

                if (receiverUser == null)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.UserNotFound, user1));
                }

                var requesterUser = context.Users
                    .Include(a => a.FriendsAdded)
                        .ThenInclude(fa => fa.Friend)
                    .Where(u => u.Username == user2)
                    .FirstOrDefault();

                if (requesterUser == null)
                {
                    throw new ArgumentException(string.Format(ErrorMessages.UserNotFound, user2));
                }

                //bool alreadyFriends = context.Friendships
                //    .Any(f => f.User == requesterUser && f.Friend == receiverUser);

                bool sender = requesterUser.FriendsAdded.Any(f => f.Friend == receiverUser);
                bool senderIsFriendOf = requesterUser.AddedAsFriendBy.Any(f => f.Friend == receiverUser);

                bool receiver = receiverUser.FriendsAdded.Any(f => f.User == requesterUser);
                bool receiverHasFriend = receiverUser.AddedAsFriendBy.Any(f => f.User == requesterUser);

                if (sender && receiver)
                {
                    throw new InvalidOperationException(string.Format(ErrorMessages.UsersAreAlreadyFriends, requesterUser, receiverUser));
                }

                if (!sender && !receiverHasFriend)
                {
                    throw new InvalidOperationException(string.Format(ErrorMessages.NotAddedFriend, requesterUser.Username, receiverUser.Username));
                }



                var friendship = new Friendship()
                {
                    User = receiverUser,
                    UserId = receiverUser.Id,
                    Friend = requesterUser,
                    FriendId = requesterUser.Id,
                };

                receiverUser.FriendsAdded.Add(friendship);
                context.SaveChanges();

                var resultMessage = string.Format(SuccessMessages.AddFriendSuccess, receiverUser.Username, requesterUser.Username);

                return resultMessage;

            }
        }
    }
}
