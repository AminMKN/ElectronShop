using _02_ElectronShopQuery.Contracts.Comment;
using _02_ElectronShopQuery.Contracts.ProductPicture;

namespace _02_ElectronShopQuery.Contracts.Product
{
    public class ProductQueryModel
    {
        public int Id { get; set; }
        public int DiscountRate { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Property { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public double Price { get; set; }
        public double PriceWithDiscount { get; set; }
        public bool HasDiscount { get; set; }
        public List<CommentQueryModel> Comments { get; set; }
        public List<ProductPictureQueryModel> Pictures { get; set; }
    }
}