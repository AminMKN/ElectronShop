using _01_Framework.Domain;
using ShopManagement.Domain.AmazingAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.ProductSubCategoryAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string Code { get; private set; }
        public string Information { get; private set; }
        public string Property { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public int CategoryId { get; private set; }
        public int SubCategoryId { get; private set; }
        public bool IsRemoved { get; private set; }
        public ProductCategory ProductCategory { get; private set; }
        public ProductSubCategory ProductSubCategory { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }
        public List<Amazing> Amazings { get; private set; }

        public Product(string name, string slug, string code, string information, string property, string description,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            int categoryId, int subCategoryId)
        {
            Name = name;
            Slug = slug;
            Code = code;
            Information = information;
            Property = property;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
            IsRemoved = false;
            Amazings = new List<Amazing>();
            ProductPictures = new List<ProductPicture>();
        }

        public void Edit(string name, string slug, string code, string information, string property, string description,
            string picture, string pictureAlt, string pictureTitle, string keywords, string metaDescription,
            int categoryId, int subCategoryId)
        {
            Name = name;
            Slug = slug;
            Code = code;
            Information = information;
            Property = property;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
            SubCategoryId = subCategoryId;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
