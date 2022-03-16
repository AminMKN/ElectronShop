using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Account.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Accounts;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public async Task<IActionResult> OnGet(AccountSearchModel searchModel)
        {
            if (ClaimChecker.CheckAccountManagementClaim())
            {
                Accounts = await _accountApplication.Search(searchModel);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }
    }
}
