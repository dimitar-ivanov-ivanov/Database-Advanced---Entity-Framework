using System;
using System.Collections.Generic;
using System.Text;
using ProductsShop.Data.Dtos.Import;
using Newtonsoft.Json;
using System.IO;
using ProductsShop.Data.Stores;
namespace ProductsShop.Export
{
    public class JsonExport
    {
        public static void ProductsInRange()
        {
            var products = ProductStore.ProductsInRange();
            var json = JsonConvert.SerializeObject(products,Formatting.Indented);
            File.WriteAllText("../exports/products-in-range.json", json);
        }

        public static void SuccessfullySoldProducts()
        {
            var users = UserStore.SuccessfullySoldProducts();
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText("../exports/users-sold-products.json", json);
        }

        public static void CategoriesByProductsCount()
        {
            var categories = CategoryStore.CategoriesByProductsCount();
            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);
            File.WriteAllText("../exports/categories-by-products.json", json);
        }

        public static void UsersAndProducts()
        {
            var userProducts = UserStore.UsersAndProducts();
            var json = JsonConvert.SerializeObject(userProducts, Formatting.Indented);
            File.WriteAllText("../exports/users-and-products.json", json);
        }
    }
}
