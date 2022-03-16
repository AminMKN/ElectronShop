using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Token).HasMaxLength(1000);
            builder.Property(x => x.ProfilePhoto).HasMaxLength(1000);

            builder
                .HasMany(x => x.AccountClaims)
                .WithOne(x => x.Account)
                .HasForeignKey(x => x.AccountId);

            builder
                .HasData(new Account(1, "مدیر سایت", "OwnerSite", "aspemail007@gmail.com", "09876543210",
                "10000.YNmL5o6NPRQENvKVLgQaww==.enVFgrgZstDmlnWaXv7o/jjjL8e8F75AUgAk5ZmQEH4=",
                "ihkecrTZxEe/wqmVG8wN/w=="));
        }
    }
}
