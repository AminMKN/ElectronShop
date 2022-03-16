using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages.Account.Profile
{
    [Authorize]
    public class OrdersModel : PageModel
    {
        public List<OrderViewModel> Orders;
        private readonly IAuthHelper _authHelper;
        private readonly IOrderApplication _orderApplication;

        public OrdersModel(IAuthHelper authHelper, IOrderApplication orderApplication)
        {
            _authHelper = authHelper;
            _orderApplication = orderApplication;
        }

        public async Task OnGet()
        {
            Orders = await _orderApplication.GetCurrentAccountOrders(_authHelper.GetCurrentAccountId());
        }
    }
}
