using ProductsShop.Data;

namespace ProductsShop.Client
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            Utility.InitDb();
        }
    }
}