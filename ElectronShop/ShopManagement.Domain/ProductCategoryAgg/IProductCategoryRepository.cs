using _01_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<int, ProductCategory>
    {
        string GetProductCategorySlug(int id);
        EditProductCategory GetDetails(int id);
        Task<List<ProductCategoryViewModel>> GetProductCategories();
        Task<List<ProductCategoryViewModel>> Search(ProductCategorySearchModel searchModel);
    }
}
