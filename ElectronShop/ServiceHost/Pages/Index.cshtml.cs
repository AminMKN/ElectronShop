using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetConfirmEmail(string userName, string token)
        {
            var result = _accountApplication.ConfirmEmail(userName, token);
            if (result.IsSuccess)
            {
                Message = ApplicationMessages.EmailConfirmed;
                return Page();
            }

            return NotFound();
        }
    }
}