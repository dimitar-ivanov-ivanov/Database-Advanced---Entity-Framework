using System;

namespace ProductsShop.Export
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonExport.ProductsInRange();
            JsonExport.SuccessfullySoldProducts();
            JsonExport.CategoriesByProductsCount();
            JsonExport.UsersAndProducts();
            XmlExport.ProductsInRange();
            XmlExport.SuccessfullySoldProducts();
            XmlExport.CategoriesByProductsCount();
            XmlExport.UsersAndProducts();
        }
    }
}
