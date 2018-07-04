using TeamBuilder.App.Core;

namespace TeamBuilder.App
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            //Utility.InitDb();
            var engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
