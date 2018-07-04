using PhotoShare.Client.Core;
using PhotoShare.Data;

namespace PhotoShare.Client
{
    public class Startup
    {
        public static void Main()
        {
            //ResetDatabase();
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }

        private static void ResetDatabase()
        {
            using (var db = new PhotoShareContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
