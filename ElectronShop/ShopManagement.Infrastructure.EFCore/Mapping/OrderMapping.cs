using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.IssueTrackingNo).HasMaxLength(8);
            builder.Property(x => x.Address).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();

            builder
                .OwnsMany(x => x.OrderItems, modelBuilder =>
                {
                    modelBuilder.ToTable("OrderItems");
                    modelBuilder.HasKey(x => x.Id);
                    modelBuilder.WithOwner(x => x.Order).HasForeignKey(x => x.OrderId);
                });
        }
    }
}
