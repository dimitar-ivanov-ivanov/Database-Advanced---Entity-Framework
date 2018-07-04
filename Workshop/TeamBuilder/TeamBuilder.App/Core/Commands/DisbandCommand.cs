namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class DisbandCommand
    {
        public String Execute(string[] args)
        {
            Check.CheckLength(1, args);
            var teamName = args[0];
            AuthenticationManager.Authorize();

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var user = AuthenticationManager.GetCurrentUser();
            if (!CommandHelper.IsUserCreatorOfTeam(teamName, user))
            {
                throw new InvalidCastException(Constants.ErrorMessages.NotAllowed);
            }

            DeleteTeam(teamName);
            return $"{teamName} has disbanded!";
        }

        public static void DeleteTeam(string name)
        {
            using(var context = new TeamBuilderContext())
            {
                var team = context.Teams.FirstOrDefault(t => t.Name == name);
                context.UserTeams.RemoveRange(context.UserTeams
                    .Where(ut => ut.Team.Name == name));

                context.EventTeams.RemoveRange(context.EventTeams
                   .Where(et => et.Team.Name == name));

                context.Invitations.RemoveRange(context.Invitations
                    .Where(i => i.Team.Name == name));

                context.Teams.Remove(team);
                context.SaveChanges();
            }
        }
    }
}
