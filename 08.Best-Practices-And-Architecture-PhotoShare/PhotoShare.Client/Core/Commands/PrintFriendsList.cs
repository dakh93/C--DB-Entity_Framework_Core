namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Messages;
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public static string Execute(string[] data)
        {
           

            using (var context = new PhotoShareContext())
            {
                var allUsers = context.Users.ToList();

                var sb = new StringBuilder();
                sb.AppendLine("Friends:");

                foreach (var user in allUsers)
                {
                    sb.AppendLine(user.Username);
                    var friends = context.Friendships
                        .Where(f => f.User == user)
                        .Select(f => f.Friend.Username)
                        .ToArray();

                    if (friends.Length < 1)
                    {
                        sb.AppendLine(ErrorMessages.NoFriends);
                    }

                    foreach (var friend in friends)
                    {
                        sb.AppendLine($"-{friend}");
                    }
                }

                return sb.ToString();
            }
        }
    }
}
