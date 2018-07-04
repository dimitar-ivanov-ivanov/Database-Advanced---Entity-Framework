namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class LoginCommand
    {
        //Login <username> <password>
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);
            var username = args[0];
            var password = args[1];

            if (AuthenticationManager.isAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            var user = this.GetUserByCredentials(username, password);
            if(user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            AuthenticationManager.Login(user);
            return $"User {username} successfully logged in!";
        }

        private User GetUserByCredentials(string username,string password)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == username &&
                u.Password == password && !u.IsDeleted);
            }
        }
    }
}
