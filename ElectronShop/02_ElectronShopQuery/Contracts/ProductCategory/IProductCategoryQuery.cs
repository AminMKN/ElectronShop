namespace _02_ElectronShopQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        Task<List<ProductCategoryQueryModel>> GetProductCategories();
        Task<ProductCategoryQueryModel> GetProductsWithProductCategory(string slug);
    }
}