using _01_Framework.Application.AuthHelper;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Admin.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        public SelectList Accounts;
        public OrderSearchModel SearchModel;
        public List<OrderViewModel> Orders;
        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IOrderApplication orderApplication, IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        public async Task<IActionResult> OnGet(OrderSearchModel searchModel)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                Accounts = new SelectList(await _accountApplication.GetAccounts(), "Id", "UserName");
                Orders = await _orderApplication.Search(searchModel);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnGetConfirm(int id)
        {
            _orderApplication.PaymentSuccess(id, 0);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(int id)
        {
            _orderApplication.Cancel(id);
            return RedirectToPage("./Index");
        }

    }
}
