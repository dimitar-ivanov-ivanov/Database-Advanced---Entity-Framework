namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class AddTeamToCommand
    {
        public String Execute(string[] args)
        {
            Check.CheckLength(2, args);
            var eventName = args[0];
            var teamName = args[1];
            AuthenticationManager.Authorize();

            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var currentUser = AuthenticationManager.GetCurrentUser();
            if (CommandHelper.IsUserCreatorOfEvent(eventName, currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            if (TeamInEvent(eventName, teamName))
            {
                throw new InvalidOperationException("Cannot add same team twice!");
            }

            AddTeamToEvent(eventName, teamName);
            return $"Team {teamName} sadded for {eventName}!";
        }

        public static bool TeamInEvent(string eventName, string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                return context.EventTeams
                    .Any(et => et.Event.Name == eventName &&
                    et.Team.Name == teamName);
            }
        }

        public static void AddTeamToEvent(string eventName,string teamName)
        {
            using(var context = new TeamBuilderContext())
            {
                var e = context.Events.FirstOrDefault(ea => ea.Name == eventName);
                var t  = context.Teams.FirstOrDefault(ta => ta.Name == teamName);

                var eventTeam = new EventTeam()
                {
                    EventId = e.Id,
                    TeamId = t.Id
                };

                context.EventTeams.Add(eventTeam);
                e.EventTeams.Add(eventTeam);
                t.EventTeams.Add(eventTeam);
                context.SaveChanges();
            }
        }
    }
}
