using AccountManagement.Domain.AccountClaimAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountClaimMapping : IEntityTypeConfiguration<AccountClaim>
    {
        public void Configure(EntityTypeBuilder<AccountClaim> builder)
        {
            builder.ToTable("AccountClaims");
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.Account)
                .WithMany(x => x.AccountClaims)
                .HasForeignKey(x => x.AccountId);

            builder
                .HasData(new AccountClaim(1, 1, true, true, true, true, true));
        }
    }
}
