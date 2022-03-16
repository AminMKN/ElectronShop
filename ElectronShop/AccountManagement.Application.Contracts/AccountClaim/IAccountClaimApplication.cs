using _01_Framework.Application;

namespace AccountManagement.Application.Contracts.AccountClaim
{
    public interface IAccountClaimApplication
    {
        OperationResult Create(CreateAccountClaim command);
        OperationResult Edit(EditAccountClaim command);
        OperationResult Remove(int id);
        EditAccountClaim GetDetails(int id);
        Task<List<AccountClaimViewModel>> Search(AccountClaimSearchModel searchModel);
    }
}
