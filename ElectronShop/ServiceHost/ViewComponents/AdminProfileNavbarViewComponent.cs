using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class AdminProfileNavbarViewComponent : ViewComponent
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;

        public AdminProfileNavbarViewComponent(IAccountApplication accountApplication, IAuthHelper authHelper)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_authHelper.IsAuthenticated())
            {
                var profile = await _accountApplication.GetCurrentAccountInfo();
                return View(profile);
            }

            return View(new AccountViewModel());
        }
    }
}
