using System;
using System.Collections.Generic;
using System.Text;
using ProductsShop.Data.Dtos.Import;
using Newtonsoft.Json;
using System.IO;
using ProductsShop.Data.Stores;

namespace ProductsShop.Import
{
    public static class JsonImport
    {
        public static void ImportUsers()
        {
            var json = File.ReadAllText("../imports/users.json");
            var users = JsonConvert.DeserializeObject<ICollection<UserDto>>(json);
            UserStore.AddUsers(users);
        }

        public static void ImportProducts()
        {
            var json = File.ReadAllText("../imports/products.json");
            var products = JsonConvert.DeserializeObject<ICollection<ProductDto>>(json);
            ProductStore.AddProducts(products);
        }

        public static void ImportCategories()
        {
            var json = File.ReadAllText("../imports/categories.json");
            var categories = JsonConvert.DeserializeObject<ICollection<CategoryDto>>(json);
            CategoryStore.AddCategories(categories);
        }
    }
}
