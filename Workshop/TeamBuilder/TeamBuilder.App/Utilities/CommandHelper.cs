namespace TeamBuilder.App.Utilities
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Invitations.Any(i => i.Team.Name == teamName
                && i.InvitedUserId == user.Id && i.IsActive == true);
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.Id == user.Id
                && u.CreatedTeams.Any(t => t.Name == teamName));
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.Id == user.Id
                && u.Events.Any(e => e.Name == eventName));
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.Username == username &&
                u.UserTeams.Any(ut=>ut.Team.Name == teamName));
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using(var context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName);
            }
        }

    }
}
