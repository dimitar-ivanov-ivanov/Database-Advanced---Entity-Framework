using ProductsShop.Data.Dtos.Export;
using ProductsShop.Data.Dtos.Import;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ProductsShop.Data.Stores
{
    public static class UserStore
    {
        public static void AddUsers(ICollection<UserDto> userDtos)
        {
            using (var context = new ProductsShopContext())
            {
                foreach (var userDto in userDtos)
                {
                    if (userDto.LastName == null)
                    {
                        Console.WriteLine("Error.Invalid data.");
                        continue;
                    }

                    var user = new User()
                    {
                        FirstName = userDto.FirstName,
                        LastName = userDto.LastName,
                        Age = userDto.Age
                    };

                    context.Users.Add(user);
                    Console.WriteLine($"Successfully imported {user.FirstName} {user.LastName}");
                }

                context.SaveChanges();
            }
        }

        public static User GetUserByName(string firstName, string lastName, ProductsShopContext context)
        {
            return context.Users.FirstOrDefault(u => u.FirstName == firstName &&
            u.LastName == lastName);
        }

        public static User GetUserById(int id, ProductsShopContext context)
        {
            return context.Users.FirstOrDefault(u => u.Id == id);
        }

        public static ICollection<UserSoldProductsDto> SuccessfullySoldProducts()
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Count != 0)
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new UserSoldProductsDto()
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        soldProducts = u.ProductsSold
                        .Select(ps => new SoldProductDto()
                        {
                            buyerFirstName = ps.Buyer == null ? null : ps.Buyer.FirstName,
                            buyerLastName = ps.Buyer == null ? null : ps.Buyer.LastName,
                            name = ps.Name,
                            price = ps.Price
                        })
                        .ToList()
                    })
                    .ToList();

                return users;
            }
        }

        public static UserProductDto UsersAndProducts()
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Count != 0)
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new UserExportDto()
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new ProductCollectionDto()
                        {
                            count = u.ProductsSold.Count,
                            products = u.ProductsSold
                            .Select(p => new ProductExportDto()
                            {
                                name = p.Name,
                                price = p.Price
                            })
                            .ToList()
                        }
                    })
                    .ToList();

                var userProductDto = new UserProductDto()
                {
                    usersCount = users.Count,
                    users = users
                };

                return userProductDto;
            }
        }

        public static XDocument SuccessfullySoldProductsXml()
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Count != 0)
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        soldProducts = u.ProductsSold
                        .Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price
                        })
                        .ToList()
                    })
                    .ToList();

                var xDoc = new XDocument();
                var usersXml = new XElement("users");

                foreach (var user in users)
                {
                    var userXml = new XElement("user",    
                        new XAttribute("last-name", user.lastName));

                    if(user.firstName != null)
                    {
                        userXml.Add(new XAttribute("first-name", user.firstName));
                    }

                    var soldProductsXml = new XElement("sold-products");

                    foreach (var product in user.soldProducts)
                    {
                        var productXml = new XElement("product",
                            new XElement("name", product.name),
                            new XElement("price", product.price));
                        soldProductsXml.Add(productXml);
                    }

                    userXml.Add(soldProductsXml);
                }

                xDoc.Add(usersXml);
                return xDoc;
            }
        }

        public static XDocument UsersAndProductsXml()
        {
            using (var context = new ProductsShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Count != 0)
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count,
                            products = u.ProductsSold
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                            .ToList()
                        }
                    })
                    .ToList();

                var userProductDto = new
                {
                    usersCount = users.Count,
                    users = users
                };

                var xDoc = new XDocument();
                var usersXml = new XElement("users",
                    new XAttribute("count", userProductDto.usersCount));

                foreach (var user in userProductDto.users)
                {
                    var userXml = new XElement("user",
                        new XAttribute("last-name", user.lastName));

                    if(user.firstName != null)
                    {
                        userXml.Add(new XAttribute("firt-name", user.firstName));
                    }

                    if (user.age != null)
                    {
                        userXml.Add(new XAttribute("age", user.age));
                    }

                    var soldProductsXml = new XElement("sold-products",
                        new XAttribute("count", user.soldProducts.count));

                    foreach (var product in user.soldProducts.products)
                    {
                        var productXml = new XElement("product",
                            new XAttribute("name", product.name),
                            new XAttribute("price", product.price));
                        soldProductsXml.Add(productXml);
                    }

                    userXml.Add(soldProductsXml);
                    usersXml.Add(userXml);
                }

                xDoc.Add(usersXml);
                return xDoc;
            }
        }
    }
}
