using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductSubCategory
{
    public interface IProductSubCategoryApplication
    {
        OperationResult Create(CreateProductSubCategory command);
        OperationResult Edit(EditProductSubCategory command);
        OperationResult Remove(int id);
        OperationResult Restore(int id);
        EditProductSubCategory GetDetails(int id);
        Task<List<ProductSubCategoryViewModel>> GetProductSubCategories();
        Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesJson(int id);
        Task<List<ProductSubCategoryViewModel>> Search(ProductSubCategorySearchModel searchModel);
    }
}
