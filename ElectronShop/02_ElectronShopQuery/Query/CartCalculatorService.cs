using _02_ElectronShopQuery.Contracts;
using DiscountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ShopCart;

namespace _02_MarketShopQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly DiscountContext _discountContext;

        public CartCalculatorService(DiscountContext discountContext)
        {
            _discountContext = discountContext;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var discount = _discountContext.Discounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToList();

            foreach (var cartItem in cartItems)
            {
                var productDiscount = discount.FirstOrDefault(x => x.ProductId == cartItem.Id);
                if (productDiscount != null)
                {
                    cartItem.DiscountRate = productDiscount.DiscountRate;
                    cartItem.DiscountAmount = cartItem.TotalAmount * cartItem.DiscountRate / 100;
                    cartItem.PayAmount = cartItem.TotalAmount - cartItem.DiscountAmount;
                    cart.Add(cartItem);
                }
                else
                {
                    cartItem.PayAmount = cartItem.TotalAmount - cartItem.DiscountAmount;
                    cart.Add(cartItem);
                }
            }

            return cart;
        }
    }
}
