using AccountManagement.Application;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.Extensions.DependencyInjection;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Infrastructure.EFCore.Repository;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.AccountClaimAgg;
using AccountManagement.Application.Contracts.AccountClaim;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();

            services.AddTransient<IAccountClaimRepository, AccountClaimRepository>();
            services.AddTransient<IAccountClaimApplication, AccountClaimApplication>();

            services.AddDbContext<AccountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
