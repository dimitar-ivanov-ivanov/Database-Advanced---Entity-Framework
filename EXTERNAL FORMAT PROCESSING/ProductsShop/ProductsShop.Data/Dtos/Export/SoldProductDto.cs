namespace ProductsShop.Data.Dtos.Export
{
    public  class SoldProductDto
    {
        public string name { get; set; }
        public decimal? price { get; set; }
        public string buyerFirstName { get; set; }
        public string buyerLastName { get; set; }
    }
}
