using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Application.Contracts.AccountClaim;
using AccountManagement.Domain.AccountClaimAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountClaimRepository : RepositoryBase<int, AccountClaim>, IAccountClaimRepository
    {
        private readonly AccountContext _context;

        public AccountClaimRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public void Remove(AccountClaim accountClaim)
        {
            _context.AccountClaims.Remove(accountClaim);
        }

        public EditAccountClaim GetDetails(int id)
        {
            return _context.AccountClaims.Select(x => new EditAccountClaim()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                ShopManagement = x.ShopManagement,
                AccountManagement = x.AccountManagement,
                CommentManagement = x.CommentManagement,
                DiscountManagement = x.DiscountManagement,
                InventoryManagement = x.InventoryManagement
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<AccountClaimViewModel>> Search(AccountClaimSearchModel searchModel)
        {
            var query = _context.AccountClaims.Select(x => new AccountClaimViewModel()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                AccountFullName = x.Account.FullName,
                AccountUserName = x.Account.UserName,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.AccountId != 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
