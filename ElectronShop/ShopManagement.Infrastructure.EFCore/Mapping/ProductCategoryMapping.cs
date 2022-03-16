using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();

            builder
                  .HasMany(x => x.ProductSubCategories)
                  .WithOne(x => x.ProductCategory)
                  .HasForeignKey(x => x.CategoryId);

            builder
                .HasMany(x => x.Products)
                .WithOne(x => x.ProductCategory)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
