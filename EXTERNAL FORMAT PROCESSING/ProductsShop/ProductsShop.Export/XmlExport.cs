using ProductsShop.Data.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProductsShop.Export
{
    public class XmlExport
    {
        public static void ProductsInRange()
        {
            var xdoc = ProductStore.ProductsInRangeXml();
            xdoc.Save("../exports/products-in-range.xml");
        }
        public static void SuccessfullySoldProducts()
        {
            var xdoc = UserStore.SuccessfullySoldProductsXml();
            xdoc.Save("../exports/users-sold-products.xml");
        }
        public static void CategoriesByProductsCount()
        {
            var xdoc = CategoryStore.CategoriesByProductsCounXml();
            xdoc.Save("../exports/categories-by-products.xml");
        }
        public static void UsersAndProducts()
        {
            var xdoc = UserStore.UsersAndProductsXml();
            xdoc.Save("../exports/users-and-products.xml");
        }
    }
}
