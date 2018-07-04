namespace P03_FootballBetting.App
{
    using P03_FootballBetting.Data;

    public class Startup
    {
        static void Main(string[] args)
        {
            using (var context = new FootballBettingContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}