namespace Instagraph.Data
{
    public class Utility
    {
        public static void InitDb()
        {
            using(var context = new InstagraphContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
