using System;
using System.Collections.Generic;
using System.Text;
using ProductsShop.Data.Dtos.Import;
using Newtonsoft.Json;
using System.IO;
using ProductsShop.Data.Stores;
using System.Xml.Linq;
using System.Linq;

namespace ProductsShop.Import
{
    public class XmlImport
    {
        public static void ImportUsers()
        {
            var users = XDocument.Load("../imports/users.xml")
                .Root.Elements()
                .Select(u => new UserDto()
                {
                    FirstName = u.Attribute("firstName")?.Value,
                    LastName = u.Attribute("lastName")?.Value
                })
                .ToList();

            UserStore.AddUsers(users);
        }

        public static void ImportProducts()
        {
            var products = XDocument.Load("../imports/products.xml")
               .Root.Elements()
               .Select(u => new ProductDto()
               {
                   Name = u.Element("name")?.Value,
                   Price = Decimal.Parse(u.Element("price")?.Value)
               })
               .ToList();

            ProductStore.AddProducts(products);
        }

        public static void ImportCategories()
        {
            var categories = XDocument.Load("../imports/categories.xml")
                       .Root.Elements()
                       .Select(u => new CategoryDto()
                       {
                           Name = u.Element("name")?.Value
                       })
                       .ToList();

            CategoryStore.AddCategories(categories);
        }
    }
}
