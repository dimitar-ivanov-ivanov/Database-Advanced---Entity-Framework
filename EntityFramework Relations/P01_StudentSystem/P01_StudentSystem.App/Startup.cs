namespace P01_StudentSystem.App
{
    using P01_StudentSystem.Data;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new StudentSystemContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}