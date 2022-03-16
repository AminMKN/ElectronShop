using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.AmazingAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class AmazingMapping : IEntityTypeConfiguration<Amazing>
    {
        public void Configure(EntityTypeBuilder<Amazing> builder)
        {
            builder.ToTable("Amazings");
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.Amazings)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
