using System.Collections.Generic;

namespace ProductsShop.Data.Dtos.Export
{
    public class UserSoldProductsDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public ICollection<SoldProductDto> soldProducts { get; set; }
    }
}
