namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[0];
            var username2 = data[1];

            using (var context = new PhotoShareContext())
            {
                var user = context.Users
                    .Include(u=>u.FriendsAdded)
                    .ThenInclude(fa=>fa.Friend)
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

                var alredyAdded = user.FriendsAdded.Any(u => u.Friend == user2);
                var accepted = user2.FriendsAdded.Any(u => u.Friend == user);

                if((alredyAdded && !accepted) || (alredyAdded && accepted))
                {
                    throw new InvalidOperationException($"{username2} is already a friend to {username}");
                }

                if(!alredyAdded && accepted)
                {
                    throw new InvalidOperationException($"{username} is already a friend to {username2}");
                }

                var friendship = new Friendship()
                {
                   User = user,
                   Friend = user2
                };

                user.FriendsAdded.Add(friendship);
                context.SaveChanges();
                return $"Friend {username2} added to {username}";
            }
        }
    }
}
