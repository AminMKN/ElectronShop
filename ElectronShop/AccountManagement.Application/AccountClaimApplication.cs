using _01_Framework.Application;
using AccountManagement.Application.Contracts.AccountClaim;
using AccountManagement.Domain.AccountClaimAgg;

namespace AccountManagement.Application
{
    public class AccountClaimApplication : IAccountClaimApplication
    {
        private readonly IAccountClaimRepository _accountClaimRepository;

        public AccountClaimApplication(IAccountClaimRepository accountClaimRepository)
        {
            _accountClaimRepository = accountClaimRepository;
        }

        public OperationResult Create(CreateAccountClaim command)
        {
            var operation = new OperationResult();
            if (_accountClaimRepository.Exists(x => x.AccountId == command.AccountId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var accountClaim = new AccountClaim(command.AccountId, command.ShopManagement, command.InventoryManagement,
                command.DiscountManagement, command.CommentManagement, command.AccountManagement);

            _accountClaimRepository.Create(accountClaim);
            _accountClaimRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditAccountClaim command)
        {
            var operation = new OperationResult();
            var accountClaim = _accountClaimRepository.Get(command.Id);
            if (accountClaim == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountClaimRepository.Exists(x => x.AccountId == command.AccountId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            accountClaim.Edit(command.AccountId, command.ShopManagement, command.InventoryManagement,
               command.DiscountManagement, command.CommentManagement, command.AccountManagement);

            _accountClaimRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var accountClaim = _accountClaimRepository.Get(id);
            if (accountClaim == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            _accountClaimRepository.Remove(accountClaim);
            _accountClaimRepository.SaveChanges();
            return operation.Success();
        }

        public EditAccountClaim GetDetails(int id)
        {
            return _accountClaimRepository.GetDetails(id);
        }

        public async Task<List<AccountClaimViewModel>> Search(AccountClaimSearchModel searchModel)
        {
            return await _accountClaimRepository.Search(searchModel);
        }
    }
}
