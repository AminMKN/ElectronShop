namespace ShopManagement.Application.Contracts.ProductSubCategory
{
    public class ProductSubCategorySearchModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
    }
}
