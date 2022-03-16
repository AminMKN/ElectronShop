using _01_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<int, Account>
    {
        Account GetAccountByUserName(string userName);
        Account GetAccountByEmail(string email);
        Task<AccountViewModel> GetCurrentAccountInfo();
        EditAccount GetDetails(int id);
        Task<List<AccountViewModel>> GetAccounts();
        Task<List<AccountViewModel>> Search(AccountSearchModel searchModel);
    }
}
