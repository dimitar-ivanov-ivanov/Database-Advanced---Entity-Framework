namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class ShowTeamCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);
            var teamName = args[0];

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            return PrintTeam(teamName);
        }

        public static string PrintTeam(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                var builder = new StringBuilder();
                var t = context.Teams.FirstOrDefault(ta => ta.Name == teamName);
                builder.AppendLine($"{t.Name} {t.Acronym}");
                builder.AppendLine("Members:");

                foreach (var m in t.UserTeams)
                {
                    builder.AppendLine($"-{m.User.UserTeams}");
                }
                return builder.ToString();
            }
        }
    }
}
