namespace TeamBuilder.App.Core.Commands
{
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class DeleteUserCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            AuthenticationManager.Authorize();
            var user = AuthenticationManager.GetCurrentUser();

            using(var context = new TeamBuilderContext())
            {
                context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                user.IsDeleted = true;
                context.SaveChanges();

                AuthenticationManager.Logout();
                return $"User {user.Username} was deleted successfully!";
            }
        }
    }
}
