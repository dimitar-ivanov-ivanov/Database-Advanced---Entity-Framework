namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Globalization;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class CreateEventCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(6, args);
            var name = args[0];
            var description = args[1];
            DateTime starDate;
            DateTime endDate;

            args[2] += " " +  args[3];
            args[4] += " "  + args[5];

            var validStarDate = DateTime.TryParseExact(args[2], Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out starDate);
            var validEndDate = DateTime.TryParseExact(args[4], Constants.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate);

            if (!validStarDate || !validEndDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (starDate > endDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.StarDateAfterEndDate);
            }

            if(name == null || name.Length > Constants.MaxEventNameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventDescriptionNotValid, name));
            }

            if (description.Length > Constants.MaxEventDescriptionLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventDescriptionNotValid, description));
            }

            AuthenticationManager.Authorize();
            RegisterEvent(name, description, starDate, endDate);
            return $"Event {name} was created successfully!";
        }

        private void RegisterEvent(string name, string description, DateTime startDate, DateTime endDate)
        {
            using (var context = new TeamBuilderContext())
            {
                var e = new Event()
                {
                    Name = name,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Creator = AuthenticationManager.GetCurrentUser()                    
                };

                context.Events.Add(e);
                context.SaveChanges();
            }
        }
    }
}


