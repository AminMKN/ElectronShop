using _01_Framework.Domain;
using ShopManagement.Application.Contracts.ProductSubCategory;

namespace ShopManagement.Domain.ProductSubCategoryAgg
{
    public interface IProductSubCategoryRepository : IRepository<int, ProductSubCategory>
    {
        string GetProductSubCategorySlug(int id);
        EditProductSubCategory GetDetails(int id);
        Task<List<ProductSubCategoryViewModel>> GetProductSubCategories();
        Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesJson(int id);
        Task<List<ProductSubCategoryViewModel>> Search(ProductSubCategorySearchModel searchModel);
    }
}
