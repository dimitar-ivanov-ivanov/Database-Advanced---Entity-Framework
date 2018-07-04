namespace MassDefect.App
{
    using MassDefect.Data;

    public class Startup
    {
        public static void Main(string[] args)
        {
            Utility.InitDb();
        }
    }
}