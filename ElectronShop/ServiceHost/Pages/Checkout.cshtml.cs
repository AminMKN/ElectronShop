using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using _01_Framework.Application.ZarinPal;
using _02_ElectronShopQuery.Contracts;
using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ShopCart;
using System.Globalization;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "ElectronShop-Cart-Items";
        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery,
            IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
        }

        public IActionResult OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            if (value == "[]")
                return RedirectToPage("/Cart");

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            if (cartItems == null)
                return RedirectToPage("/Cart");

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(Cart);
            return Page();
        }

        public async Task<IActionResult> OnPostPay(string postalCode, string address)
        {
            var cart = _cartService.Get();
            var result = await _productQuery.CheckInventoryStatus(cart.CartItems);
            if (result.Count == 0)
                return RedirectToPage("/Cart");

            if (result.Any(x => !x.InStock))
                return RedirectToPage("/Cart");

            var phoneNumber = _authHelper.GetCurrentAccountPhoneNumber();
            cart.PhoneNumber = phoneNumber;
            cart.PostalCode = postalCode;
            cart.Address = address;
            var userEmail = _authHelper.GetCurrentAccountEmail();
            var orderId = _orderApplication.PlaceOrder(cart);
            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(CultureInfo.InvariantCulture),
                 cart.PhoneNumber, userEmail, "خرید از سایت الکترون شاپ", orderId);

            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status, [FromQuery] int oId)
        {
            var orderAmount = _orderApplication.GetAmount(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApplication.PaymentSuccess(oId, verificationResponse.RefId);
                Response.Cookies.Delete(CookieName);
                result = result.Success(ApplicationMessages.PaymentSuccess, issueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }

            result = result.Failed(ApplicationMessages.PaymentFailed);
            return RedirectToPage("/PaymentResult", result);
        }
    }
}
