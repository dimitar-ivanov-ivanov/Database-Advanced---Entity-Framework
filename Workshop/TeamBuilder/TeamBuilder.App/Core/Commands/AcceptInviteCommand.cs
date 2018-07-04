namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class AcceptInviteCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);
            AuthenticationManager.Authorize();
            var team = GetTeam(args[0]);
            var user = AuthenticationManager.GetCurrentUser();

            if (team == null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, team.Name));
            }

            if (!CommandHelper.IsInviteExisting(team.Name, user))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, team.Name));
            }

            AcceptInvite(team, user);
            return $"User {user.Username} joined team {team.Name}!";
        }

        private static Team GetTeam(string name)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Teams.FirstOrDefault(t => t.Name == name);
            }
        }

        private static void AcceptInvite(Team team, User user)
        {
            using (var context = new TeamBuilderContext())
            {
                var invite = context.Invitations.FirstOrDefault(i => i.Team.Name == team.Name &&
                i.InvitedUser.Username == user.Username);
                invite.IsActive = false;

                var userTeam = new UserTeam()
                {
                    TeamId = team.Id,
                    UserId = user.Id
                };

                team.UserTeams.Add(userTeam);
                user.UserTeams.Add(userTeam);
                context.UserTeams.Add(userTeam);
                context.SaveChanges();
            }
        }
    }
}
