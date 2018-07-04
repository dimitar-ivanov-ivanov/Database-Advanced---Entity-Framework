namespace TeamBuilder.App.Core
{
    using System;

    public class Engine
    {
        private CommandDispatcher commandDispatcher;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var output = this.commandDispatcher.Dispatch(input);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(output);
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(e.GetBaseException().Message);
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
