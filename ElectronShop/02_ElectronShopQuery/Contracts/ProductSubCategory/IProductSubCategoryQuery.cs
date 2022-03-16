namespace _02_ElectronShopQuery.Contracts.ProductSubCategory
{
    public interface IProductSubCategoryQuery
    {
        Task<ProductSubCategoryQueryModel> GetProductsWithProductSubCategory(string slug);
    }
}
