using ShopManagement.Application.Contracts.ShopCart;

namespace _02_ElectronShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        Task<List<ProductQueryModel>> GetProducts();
        Task<List<ProductQueryModel>> GetProductsHaveDiscount();
        Task<List<ProductQueryModel>> GetAmazings(int position);
        Task<List<ProductQueryModel>> Search(string search);
        Task<ProductQueryModel> GetProductDetails(string slug);
        Task<List<CartItem>> CheckInventoryStatus(List<CartItem> cartItems);
    }
}
