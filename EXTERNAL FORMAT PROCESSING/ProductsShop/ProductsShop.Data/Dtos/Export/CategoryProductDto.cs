namespace ProductsShop.Data.Dtos.Export
{
    public class CategoryProductDto
    {
        public string category { get; set; }
        public int productsCount { get; set; }
        public decimal averagePrice { get; set; }
        public decimal totalRevenue { get; set; }
    }
}
