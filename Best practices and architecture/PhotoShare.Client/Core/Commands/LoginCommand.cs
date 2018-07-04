namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LoginCommand
    {
        public string Execute(string[] input, HashSet<string> loggedUsers)
        {
            using (var context = new PhotoShareContext())
            {
                var username = input[0];
                var password = input[1];

                var user = context.Users
                    .FirstOrDefault(u => u.Username == username &&
                    u.Password == password);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password!");
                }

                if (loggedUsers.Contains(username))
                {
                    throw new ArgumentException("You should logout first!");
                }

                loggedUsers.Add(username);
                return $"User {username} successfully logged in!";
            }
        }
    }
}
