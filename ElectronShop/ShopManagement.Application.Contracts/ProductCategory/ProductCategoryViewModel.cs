namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
