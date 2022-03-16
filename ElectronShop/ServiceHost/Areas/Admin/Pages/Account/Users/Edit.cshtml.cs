using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Account.Users
{
    [Authorize]
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public EditAccount Command;
        private readonly IAccountApplication _accountApplication;

        public EditModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public IActionResult OnGet(int id)
        {
            if (ClaimChecker.CheckAccountManagementClaim())
            {
                Command = _accountApplication.GetDetails(id);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnPost(EditAccount command)
        {
            if (!ModelState.IsValid)
            {
                OnGet(command.Id);
                return Page();
            }

            var result = _accountApplication.Edit(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            OnGet(command.Id);
            return Page();
        }
    }
}
