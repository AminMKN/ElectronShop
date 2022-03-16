using _01_Framework.Application;
using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.ShopCart;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public string EmptyCart { get; set; }
        public const string CookieName = "ElectronShop-Cart-Items";
        public List<CartItem> CartItems { get; set; }
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
            CartItems = new List<CartItem>();
        }

        public async Task<IActionResult> OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            if (value == "[]")
            {
                EmptyCart = ApplicationMessages.EmptyCart;
                return Page();
            }

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            if (cartItems == null)
            {
                EmptyCart = ApplicationMessages.EmptyCart;
                return Page();
            }

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            CartItems = await _productQuery.CheckInventoryStatus(cartItems);
            return Page();
        }

        public IActionResult OnGetRemoveFromCart(int id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(5) };
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            return RedirectToPage("/Cart");
        }

        public async Task<IActionResult> OnGetGoToCheckout()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            if (value == "[]")
            {
                EmptyCart = ApplicationMessages.EmptyCart;
                return Page();
            }

            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            if (cartItems == null)
            {
                EmptyCart = ApplicationMessages.EmptyCart;
                return Page();
            }

            foreach (var item in cartItems)
                item.CalculateTotalItemPrice();

            CartItems = await _productQuery.CheckInventoryStatus(cartItems);
            if (CartItems.Any(x => !x.InStock))
                return RedirectToPage("/Cart");

            return RedirectToPage("/Checkout");
        }
    }
}
