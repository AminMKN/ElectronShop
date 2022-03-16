using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.AccountClaimAgg;
using AccountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore
{
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountClaim> AccountClaims { get; set; }

        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var assembly = typeof(AccountMapping).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);
        }
    }
}
