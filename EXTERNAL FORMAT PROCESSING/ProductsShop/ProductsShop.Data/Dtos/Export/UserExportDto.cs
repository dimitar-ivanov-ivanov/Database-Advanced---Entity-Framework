namespace ProductsShop.Data.Dtos.Export
{
    public class UserExportDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int? age { get; set; }
        public ProductCollectionDto soldProducts { get; set; }
    }
}
