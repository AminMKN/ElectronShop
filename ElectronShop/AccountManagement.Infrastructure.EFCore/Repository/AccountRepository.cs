using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using _01_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<int, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        private readonly IAuthHelper _authHelper;

        public AccountRepository(AccountContext context, IAuthHelper authHelper) : base(context)
        {
            _context = context;
            _authHelper = authHelper;
        }

        public Account GetAccountByUserName(string userName)
        {
            return _context.Accounts.Include(x=>x.AccountClaims).FirstOrDefault(x => x.UserName == userName);
        }

        public Account GetAccountByEmail(string email)
        {
            return _context.Accounts.Include(x => x.AccountClaims).FirstOrDefault(x => x.Email == email);
        }
        
        public async Task<AccountViewModel> GetCurrentAccountInfo()
        {
            return await _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                ProfilePhoto = x.ProfilePhoto,
                EmailConfirmed = x.EmailConfirmed,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsNoTracking().FirstOrDefaultAsync(x => x.Id == _authHelper.GetCurrentAccountId());
        }

        public EditAccount GetDetails(int id)
        {
            return _context.Accounts.Select(x => new EditAccount()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<AccountViewModel>> GetAccounts()
        {
            return await _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                UserName = x.UserName
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<AccountViewModel>> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Select(x => new AccountViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                ProfilePhoto = x.ProfilePhoto,
                EmailConfirmed = x.EmailConfirmed,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                query = query.Where(x => x.UserName.Contains(searchModel.UserName));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
