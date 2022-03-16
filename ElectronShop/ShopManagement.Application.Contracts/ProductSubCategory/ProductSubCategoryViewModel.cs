namespace ShopManagement.Application.Contracts.ProductSubCategory
{
    public class ProductSubCategoryViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
