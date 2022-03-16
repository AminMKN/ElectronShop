using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Account.Profile
{
    [Authorize]
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        public EditAccount Command;
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;

        public EditModel(IAuthHelper authHelper, IAccountApplication accountApplication)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
            Command = _accountApplication.GetDetails(_authHelper.GetCurrentAccountId());
        }

        public IActionResult OnPost(EditAccount command)
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            var result = _accountApplication.Edit(command);
            if (result.IsSuccess)
                return RedirectToPage("/Account/Profile/Index");

            Message = result.Message;
            OnGet();
            return Page();
        }
    }
}
