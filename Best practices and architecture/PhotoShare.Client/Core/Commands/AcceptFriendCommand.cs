namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[0];
            var username2 = data[1];

            using (var context = new PhotoShareContext())
            {
                var user = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == username);

                var user2 = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == username2);

                if (user == null)
                {
                    throw new ArgumentException($"{username} not found!");
                }
                if (user2 == null)
                {
                    throw new ArgumentException($"{username2} not found!");
                }

                //var user1Adds = user.FriendsAdded.Any(u => u.Friend == user2);
                //var user1IsAdded = user.AddedAsFriendBy.Any(u => u.Friend == user2);

                var added = user2.FriendsAdded.Any(u => u.Friend == user);
                var accepted = user2.AddedAsFriendBy.Any(u => u.Friend == user);

                if (added && accepted)
                {
                    throw new InvalidOperationException($"{username2} is already a friend to {username}");
                }

                if (!added)
                {
                    throw new InvalidOperationException($"{username2} has not added {username} as a friend");
                }

                var friendship = new Friendship()
                {
                    User = user2,
                    Friend = user
                };

                user2.FriendsAdded.Add(friendship);
                context.SaveChanges();
                return $"{username} accepted {username2} as a friend";
            }
        }
    }
}
