using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.AccountClaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Admin.Pages.Account.UserClaims
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public SelectList Accounts;
        public AccountClaimSearchModel SearchModel;
        public List<AccountClaimViewModel> AccountClaims;
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountClaimApplication _accountClaimApplication;

        public IndexModel(IAccountApplication accountApplication, IAccountClaimApplication accountClaimApplication)
        {
            _accountApplication = accountApplication;
            _accountClaimApplication = accountClaimApplication;
        }

        public async Task<IActionResult> OnGet(AccountClaimSearchModel searchModel)
        {
            if (ClaimChecker.CheckAccountManagementClaim())
            {
                Accounts = new SelectList(await _accountApplication.GetAccounts(), "Id", "UserName");
                AccountClaims = await _accountClaimApplication.Search(searchModel);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnGetRemove(int id)
        {
            _accountClaimApplication.Remove(id);
            return RedirectToPage("./Index");
        }
    }
}
