using Microsoft.EntityFrameworkCore;
using ProductsShop.Models;

namespace ProductsShop.Data
{
    public class ProductsShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public ProductsShopContext() { }

        public ProductsShopContext(DbContextOptions options)
         : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });

            builder.Entity<CategoryProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(p => p.CategoryProducts)
                .HasForeignKey(cp=>cp.ProductId);

            builder.Entity<CategoryProduct>()
               .HasOne(cp => cp.Category)
               .WithMany(c => c.CategoryProducts)
               .HasForeignKey(cp => cp.CategoryId);

            builder.Entity<User>()
               .HasMany(u => u.ProductsBought)
               .WithOne(p => p.Buyer)
               .HasForeignKey(p=>p.BuyerId);

            builder.Entity<User>()
               .HasMany(u => u.ProductsSold)
               .WithOne(p => p.Seller)
               .HasForeignKey(p => p.SellerId);
        }
    }
}
