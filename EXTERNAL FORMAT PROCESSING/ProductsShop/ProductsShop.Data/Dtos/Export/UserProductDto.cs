using System.Collections.Generic;

namespace ProductsShop.Data.Dtos.Export
{
    public class UserProductDto
    {
        public int usersCount { get; set; }
        public ICollection<UserExportDto> users;
    }
}
