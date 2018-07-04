namespace Instagraph.App
{
    using Instagraph.Data;

    public class Startup
    {
        public static void Main(string[] args)
        {
            Utility.InitDb();
        }
    }
}
