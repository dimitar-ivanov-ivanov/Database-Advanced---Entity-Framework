namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    public class ShowEventCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(1, args);
            var eventName = args[0];

            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }
            return PrintEvent(eventName);
        }

        public static string PrintEvent(string eventName)
        {
            using (var context = new TeamBuilderContext())
            {
                var builder = new StringBuilder();
                var e = context.Events.FirstOrDefault(ea => ea.Name == eventName);
                builder.AppendLine($"{e.Name} {e.StartDate} {e.EndDate}");
                builder.AppendLine(e.Description);
                builder.AppendLine("Teams:");

                foreach (var t in e.EventTeams)
                {
                    builder.AppendLine($"-{t.Team.Name}");
                }
                return builder.ToString();
            }
        }
    }
}
