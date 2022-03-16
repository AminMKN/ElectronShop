namespace ShopManagement.Application.Contracts.Product
{
    public class ProductSearchModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
