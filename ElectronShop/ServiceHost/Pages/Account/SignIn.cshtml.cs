using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account
{
    public class SignInModel : PageModel
    {
        public string Message { get; set; }
        public SignInAccount Command;
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;

        public SignInModel(IAccountApplication accountApplication, IAuthHelper authHelper)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
        }

        public IActionResult OnGet()
        {
            if (_authHelper.IsAuthenticated())
                return RedirectToPage("/Index");

            return Page();
        }

        public IActionResult OnPost(SignInAccount command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = _accountApplication.SignIn(command);
            if (result.IsSuccess)
                return RedirectToPage("/Index");

            Message = ApplicationMessages.UserNameOrPasswordNotValid;
            return Page();
        }
    }
}
