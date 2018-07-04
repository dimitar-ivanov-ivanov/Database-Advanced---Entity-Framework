namespace TeamBuilder.App.Core.Commands
{
    using TeamBuilder.App.Utilities;

    public class LogoutCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            AuthenticationManager.Authorize();
            var user = AuthenticationManager.GetCurrentUser();
            AuthenticationManager.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}
