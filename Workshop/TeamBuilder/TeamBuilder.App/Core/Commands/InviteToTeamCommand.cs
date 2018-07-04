namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class InviteToTeamCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(2, args);
            var team = args[0];
            var username = args[1];

            AuthenticationManager.Authorize();
            var user = GetUser(username);

            var loggedUser = AuthenticationManager.GetCurrentUser();
            var t = GetTeam(team);

            if (CommandHelper.IsUserCreatorOfTeam(team, user))
            {
                AddUserToTeam(user, t);
            }

            if (!CommandHelper.IsUserCreatorOfTeam(team, loggedUser) &&
                !CommandHelper.IsMemberOfTeam(team, loggedUser.Username))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            if (!CommandHelper.IsUserExisting(username) || !CommandHelper.IsTeamExisting(team))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            if (CommandHelper.IsInviteExisting(team, user))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }

            RegisterInvite(user, t);
            return $"Team {team} invited {username}!";
        }

        private static User GetUser(string name)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == name);
            }
        }

        private static Team GetTeam(string name)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Teams.FirstOrDefault(t => t.Name == name);
            }
        }

        private static void AddUserToTeam(User user, Team team)
        {
            using (var context = new TeamBuilderContext())
            {
                var userTeam = new UserTeam()
                {
                    Team = team,
                    TeamId =team.Id,
                    User = user,
                    UserId = user.Id
                };

                context.UserTeams.Add(userTeam);
                user.UserTeams.Add(userTeam);
                team.UserTeams.Add(userTeam);
                context.SaveChanges();
            }
        }

        private static void RegisterInvite(User user, Team team)
        {
            using (var context = new TeamBuilderContext())
            {
                var invite = new Invitation()
                {
                    TeamId = team.Id,
                    InvitedUserId = user.Id,
                    IsActive = true
                };

                context.Invitations.Add(invite);
                context.SaveChanges();
                user.Invitations.Add(invite);
                team.Invitations.Add(invite); 
                context.SaveChanges();
            }
        }
    }
}
