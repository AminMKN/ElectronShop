using _02_ElectronShopQuery.Contracts.Product;
using _02_ElectronShopQuery.Contracts.ProductSubCategory;

namespace _02_ElectronShopQuery.Contracts.ProductCategory
{
    public class ProductCategoryQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductQueryModel> Products { get; set; }
        public List<ProductSubCategoryQueryModel> ProductSubCategories { get; set; }
    }
}