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
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList Accounts;
        public CreateAccountClaim Command;
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountClaimApplication _accountClaimApplication;

        public CreateModel(IAccountApplication accountApplication, IAccountClaimApplication accountClaimApplication)
        {
            _accountApplication = accountApplication;
            _accountClaimApplication = accountClaimApplication;
        }

        public async Task<IActionResult> OnGet()
        {
            if (ClaimChecker.CheckAccountManagementClaim())
            {
                Accounts = new SelectList(await _accountApplication.GetAccounts(), "Id", "UserName");
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }


        public async Task<IActionResult> OnPost(CreateAccountClaim command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            var result = _accountClaimApplication.Create(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            await OnGet();
            return Page(); 
        }
    }
}
