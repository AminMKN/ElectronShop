using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.AmazingAgg;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.ProductSubCategoryAgg;
using ShopManagement.Infrastructure.EFCore.Mapping;

namespace ShopManagement.Infrastructure.EFCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Amazing> Amazings { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var assembly = typeof(ProductCategoryMapping).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);
        }
    }
}