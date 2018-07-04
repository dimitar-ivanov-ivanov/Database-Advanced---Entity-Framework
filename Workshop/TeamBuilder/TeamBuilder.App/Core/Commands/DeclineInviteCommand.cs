namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class DeclineInviteCommand
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

            DeleteInvite(team, user);
            return $"Invite from {team.Name} declined.";
        }

        private void DeleteInvite(Team team, User user)
        {
            using(var context = new TeamBuilderContext())
            {
                var invite = context.Invitations
                    .FirstOrDefault(i => i.Team.Name == team.Name &&
                    i.InvitedUser.Username == user.Username);

                invite.IsActive = false;
                context.SaveChanges();
            }
        }

        private static Team GetTeam(string name)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Teams.FirstOrDefault(t => t.Name == name);
            }
        }
    }
}
