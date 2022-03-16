using ShopManagement.Application.Contracts.ShopCart;

namespace _02_ElectronShopQuery.Contracts
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
