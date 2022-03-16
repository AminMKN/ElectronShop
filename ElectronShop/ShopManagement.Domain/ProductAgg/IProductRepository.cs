using _01_Framework.Domain;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<int, Product>
    {
        Product GetProductWithProductCategory(int id);
        EditProduct GetDetails(int id);
        Task<List<ProductViewModel>> GetProducts();
        Task<List<ProductViewModel>> Search(ProductSearchModel searchModel);
    }
}
