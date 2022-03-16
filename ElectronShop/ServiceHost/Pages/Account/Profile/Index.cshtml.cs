using _01_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public AccountViewModel Command;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public async Task OnGet()
        {
            Command = await _accountApplication.GetCurrentAccountInfo();
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _accountApplication.SendEmailConfirmation();
            Message = result.Message;
            await OnGet();
            return Page();
        }
    }
}
