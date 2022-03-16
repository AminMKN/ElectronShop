using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        public string Message { get; set; }
        public ForgotPassword Command;
        private readonly IAccountApplication _accountApplication;

        public ForgotPasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(ForgotPassword command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _accountApplication.ForgotPassword(command);
            Message = result.Message;
            return Page();
        }
    }
}
