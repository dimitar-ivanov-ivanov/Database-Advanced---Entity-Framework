using ProductsShop.Data.Dtos.Export;
using ProductsShop.Data.Dtos.Import;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ProductsShop.Data.Stores
{
    public static class ProductStore
    {
        public static void AddProducts(ICollection<ProductDto> productDtos)
        {
            using (var context = new ProductsShopContext())
            {
                var random = new Random();
                foreach (var productDto in productDtos)
                {
                    if (productDto.Name == null || productDto.Price == null)
                    {
                        Console.WriteLine("Error.Invalid data.");
                        continue;
                    }

                    var buyerId = random.Next(1, context.Users.Count());
                    var sellerId = buyerId;

                    while (buyerId == sellerId)
                    {
                        sellerId = random.Next(1, context.Users.Count());
                    }

                    var buyer = UserStore.GetUserById(buyerId, context);
                    var seller = UserStore.GetUserById(sellerId, context);

                    var product = new Product()
                    {
                        Name = productDto.Name,
                        Price = (decimal)productDto.Price,
                        Buyer = buyer,
                        Seller = seller
                    };

                    context.Products.Add(product);
                    buyer.ProductsBought.Add(product);
                    seller.ProductsSold.Add(product);
                    Console.WriteLine($"Sucessfully imported prodcut {product.Name}");
                }

                context.SaveChanges();
            }
        }

        public static Product GetProductByName(string name, ProductsShopContext context)
        {
            return context.Products.FirstOrDefault(p => p.Name == name);
        }

        public static Product GetProductById(int id, ProductsShopContext context)
        {
            return context.Products.FirstOrDefault(p => p.Id == id);
        }

        public static ICollection<ProductInRangeDto> ProductsInRange()
        {
            using (var context = new ProductsShopContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new ProductInRangeDto()
                    {
                        name = p.Name,
                        price = p.Price,
                        seller = p.Seller == null ? null : p.Seller.FirstName + " " + p.Seller.LastName
                    })
                    .ToList();

                return products;
            }
        }

        public static XDocument ProductsInRangeXml()
        {
            using (var context = new ProductsShopContext())
            {
                var products = context.Products
                     .Where(p => p.Price >= 500 && p.Price <= 1000 && p.Buyer != null)
                     .OrderBy(p => p.Price)
                     .Select(p => new
                     {
                         name = p.Name,
                         price = p.Price,
                         buyer = p.Buyer.FirstName ?? "" + " " + p.Buyer.LastName ?? ""
                     })
                     .ToList();

                var xDoc = new XDocument();
                var productsXml = new XElement("products");

                foreach (var product in products)
                {
                    var productXml = new XElement("product",
                        new XAttribute("name", product.name),
                        new XAttribute("price", product.price),
                        new XAttribute("buyer", product.buyer));
                    productsXml.Add(productXml);
                }

                xDoc.Add(productsXml);
                return xDoc;
            }
        }
    }
}
