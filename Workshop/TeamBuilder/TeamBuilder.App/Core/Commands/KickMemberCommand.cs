namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class KickMemberCommand
    {
        public String Execute(String[] args)
        {
            var teamName = args[0];
            var userName = args[1];

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            if (!CommandHelper.IsUserExisting(userName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, userName));
            }

            if (!CommandHelper.IsMemberOfTeam(teamName, userName))
            {
                throw new ArgumentException($"User {userName} is not a member in {teamName}!");
            }

            var currentUser = AuthenticationManager.GetCurrentUser();
            if (!CommandHelper.IsUserCreatorOfTeam(teamName, currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            if (currentUser.Username == userName)
            {
                throw new InvalidOperationException($"Command not allowed. Use DisbandTeam instead.");
            }

            KickMember(teamName,userName);
            return $"User {userName} was kicked from {teamName}!";
        }


        public static void KickMember(string teamName, string userName)
        {
            using (var context = new TeamBuilderContext())
            {
                var team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                var user = context.Users.First(u => u.Username == userName);

                var userTeam = context.UserTeams.FirstOrDefault(ut => ut.User.Username == userName &&
                    ut.Team.Name == teamName);

                team.UserTeams.Remove(userTeam);
                user.UserTeams.Remove(userTeam);
                context.UserTeams.Remove(userTeam);
                context.SaveChanges();
            }
        }
    }
}
