using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account
{
    public class SignUpModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SignUpAccount Command;
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;

        public SignUpModel(IAccountApplication accountApplication, IAuthHelper authHelper)
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
