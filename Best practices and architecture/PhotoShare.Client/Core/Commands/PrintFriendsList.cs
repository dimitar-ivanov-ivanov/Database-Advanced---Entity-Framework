namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintFriendsListCommand 
    {
        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
          using(var context = new PhotoShareContext())
            {
                var username = data[0];
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if(user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                var commonFriends = context.Users
                    .Include(u => u.FriendsAdded)
                    .SingleOrDefault(u => u.Username == username)
                    .FriendsAdded
                    .Select(f => f.Friend.Username)    // username only
                    .OrderBy(u => u)            // ASC
                    .ToList();

                if (commonFriends.Count == 0)
                {
                    return "No friends for this user. :(";
                }

                var builder = new StringBuilder();
                builder.AppendLine("Friends:");
                foreach (var u in commonFriends)
                {
                    builder.AppendLine($"-{u}");
                }

                return builder.ToString();
            }
        }
    }
}
