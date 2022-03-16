namespace ShopManagement.Application.Contracts.ShopCart
{
    public interface ICartService
    {
        void Set(Cart cart);
        Cart Get();
    }
}
