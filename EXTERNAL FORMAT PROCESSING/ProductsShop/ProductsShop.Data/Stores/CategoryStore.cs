using ProductsShop.Data.Dtos.Export;
using ProductsShop.Data.Dtos.Import;
using ProductsShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ProductsShop.Data.Stores
{
    public static class CategoryStore
    {
        public static void AddCategories(ICollection<CategoryDto> categoryDtos)
        {
            using (var context = new ProductsShopContext())
            {
                foreach (var categoryDto in categoryDtos)
                {
                    if (categoryDto.Name == null || categoryDto.Name.Length < 3 ||
                        categoryDto.Name.Length > 15)
                    {
                        Console.WriteLine("Error.Invalid data.");
                        continue;
                    }

                    var category = new Category()
                    {
                        Name = categoryDto.Name
                    };

                    var categoryProducts = new List<CategoryProduct>();
                    foreach (var product in context.Products)
                    {
                        var categoryProduct = new CategoryProduct()
                        {
                            Category = category,
                            Product = product
                        };

                        categoryProducts.Add(categoryProduct);
                        category.CategoryProducts.Add(categoryProduct);
                    }

                    context.Categories.Add(category);
                    Console.WriteLine($"Successfully imported category {categoryDto.Name}");
                }

                context.SaveChanges();
            }
        }

        public static Category GetCategoryByName(string name, ProductsShopContext context)
        {
            return context.Categories.FirstOrDefault(c => c.Name == name);
        }

        public static ICollection<CategoryProductDto> CategoriesByProductsCount()
        {
            using(var context = new ProductsShopContext())
            {
                var categories = context.Categories
                    .Select(c => new CategoryProductDto()
                    {
                        category = c.Name,
                        productsCount = c.CategoryProducts.Count,
                        averagePrice = c.CategoryProducts.Average(cp=>cp.Product.Price),
                        totalRevenue = c.CategoryProducts.Sum(cp=>cp.Product.Price)
                    })
                    .ToList();

                return categories;
            }
        }

        public static XDocument CategoriesByProductsCounXml()
        {
            using (var context = new ProductsShopContext())
            {
                var categories = context.Categories
                    .Select(c => new 
                    {
                        category = c.Name,
                        productsCount = c.CategoryProducts.Count,
                        averagePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                        totalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                    })
                    .ToList();

                var xdoc = new XDocument();
                var categoriesXml = new XElement("categories");

                foreach (var category in categories)
                {
                    var categoryXml = new XElement("category",
                        new XAttribute("name", category.category),
                        new XElement("products-count",category.productsCount),
                        new XElement("average-price",category.averagePrice),
                        new XElement("total-revenue",category.totalRevenue));

                    categoriesXml.Add(categoryXml);
                }

                xdoc.Add(categoriesXml);
                return xdoc;
            }
        }
    }
}
