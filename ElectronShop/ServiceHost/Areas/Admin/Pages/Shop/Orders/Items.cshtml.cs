using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Areas.Admin.Pages.Shop.Orders
{
    public class ItemsModel : PageModel
    {
        public List<OrderItemViewModel> OrderItems;
        private readonly IOrderApplication _orderApplication;

        public ItemsModel(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                OrderItems = await _orderApplication.GetOrderItems(id);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }
    }
}
