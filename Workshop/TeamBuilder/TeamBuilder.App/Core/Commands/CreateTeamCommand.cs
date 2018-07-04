namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class CreateTeamCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(3, args);
            AuthenticationManager.isAuthenticated();
            var name = args[0];
            var acronym = args[1];
            var description = args[2];

            if (name == null || name.Length > Constants.MaxTeamNameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNameNotValid, name));
            }

            if (description != null && description.Length > Constants.MaxTeamDescriptionLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamDescriptionNotValid, description));
            }

            if (acronym == null || acronym.Length != Constants.TeamAcronymLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamAcronymNotValid, acronym));
            }

            var team = GetTeam(name);
            if (team != null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists, name));
            }

            AuthenticationManager.Authorize();
            RegiterTeam(name, acronym, description);
            return $"Team {name} successfully created!";
        }

        private Team GetTeam(string name)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.Teams.FirstOrDefault(t => t.Name == name);
            }
        }

        private void RegiterTeam(string name, string acronym, string description)
        {
            using (var context = new TeamBuilderContext())
            {
                var user = AuthenticationManager.GetCurrentUser();
                var team = new Team()
                {
                    Acronym = acronym,
                    CreatorId = user.Id,
                    Description = description,
                    Name = name,
                };

                context.Teams.Add(team);
                user.CreatedTeams.Add(team);
                context.SaveChanges();
            }
        }
    }
}
