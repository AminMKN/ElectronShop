using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account
{
    public class SignOutModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public SignOutModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public IActionResult OnGet()
        {
            _accountApplication.SignOut();
            return RedirectToPage("/Account/SignIn");
        }
    }
}
