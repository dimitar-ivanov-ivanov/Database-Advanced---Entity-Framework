namespace MassDefect.Data
{
    public static class Utility
    {
        public static void InitDb()
        {
            using (var context = new MassDefectContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
