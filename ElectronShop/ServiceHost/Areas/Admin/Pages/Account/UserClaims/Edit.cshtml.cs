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
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList Accounts;
        public EditAccountClaim Command;
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountClaimApplication _accountClaimApplication;

        public EditModel(IAccountApplication accountApplication, IAccountClaimApplication accountClaimApplication)
        {
            _accountApplication = accountApplication;
            _accountClaimApplication = accountClaimApplication;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (ClaimChecker.CheckAccountManagementClaim())
            {
                Accounts = new SelectList(await _accountApplication.GetAccounts(), "Id", "UserName");
                Command = _accountClaimApplication.GetDetails(id);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(EditAccountClaim command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet(command.Id);
                return Page();
            }

            var result = _accountClaimApplication.Edit(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            await OnGet(command.Id);
            return Page();
        }
    }
}
