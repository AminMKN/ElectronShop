namespace ShopManagement.Application.Contracts.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Picture { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
