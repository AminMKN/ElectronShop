using _01_Framework.Domain;
using AccountManagement.Application.Contracts.AccountClaim;

namespace AccountManagement.Domain.AccountClaimAgg
{
    public interface IAccountClaimRepository : IRepository<int, AccountClaim>
    {
        void Remove(AccountClaim accountClaim);
        EditAccountClaim GetDetails(int id);
        Task<List<AccountClaimViewModel>> Search(AccountClaimSearchModel searchModel);
    }
}
