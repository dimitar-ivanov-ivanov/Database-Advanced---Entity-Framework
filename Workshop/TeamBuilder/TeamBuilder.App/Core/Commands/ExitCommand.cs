namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Utilities;

    public class ExitCommand
    {
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            Environment.Exit(0);
            return "Bye!";
        }
    }
}
