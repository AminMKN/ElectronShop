using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.Name).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Keywords).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Property).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(4000).IsRequired();
            builder.Property(x => x.Information).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();

            builder
                .HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            builder
                .HasOne(x => x.ProductSubCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SubCategoryId);

            builder
                .HasMany(x => x.Amazings)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            builder
                .HasMany(x => x.ProductPictures)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
