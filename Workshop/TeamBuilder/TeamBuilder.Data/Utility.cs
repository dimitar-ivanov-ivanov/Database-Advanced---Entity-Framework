namespace TeamBuilder.Data
{
    public static class Utility
    {
        public static void InitDb()
        {
            using (var context = new TeamBuilderContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
