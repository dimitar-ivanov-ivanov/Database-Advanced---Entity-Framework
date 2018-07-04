using System.Collections.Generic;

namespace ProductsShop.Data.Dtos.Export
{
    public class ProductCollectionDto
    {
        public int count { get; set; }
        public ICollection<ProductExportDto> products { get; set; }
    }
}
