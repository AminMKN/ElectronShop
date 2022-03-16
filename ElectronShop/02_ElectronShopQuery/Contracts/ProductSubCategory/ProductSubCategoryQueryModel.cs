using _02_ElectronShopQuery.Contracts.Product;

namespace _02_ElectronShopQuery.Contracts.ProductSubCategory
{
    public class ProductSubCategoryQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }
}
