using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Account.Users
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SignUpAccount Command;
        private readonly IAccountApplication _accountApplication;

        public CreateModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public IActionResult OnGet()
        {
            if (ClaimChecker.CheckAccountManagementClaim())
            {
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnPost(SignUpAccount command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = _accountApplication.SignUp(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            return Page();
        }
    }
}
