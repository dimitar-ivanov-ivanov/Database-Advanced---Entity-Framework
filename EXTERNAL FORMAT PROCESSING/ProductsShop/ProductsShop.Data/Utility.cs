namespace ProductsShop.Data
{
    public static class Utility
    {
        public static void InitDb()
        {
            using (var context = new ProductsShopContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
