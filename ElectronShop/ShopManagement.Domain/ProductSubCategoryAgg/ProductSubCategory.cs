using _01_Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Domain.ProductSubCategoryAgg
{
    public class ProductSubCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public int CategoryId { get; private set; }
        public bool IsRemoved { get; private set; }
        public ProductCategory ProductCategory { get; private set; }
        public List<Product> Products { get; private set; }

        public ProductSubCategory(string name, string slug, string keywords, string metaDescription, int categoryId)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
            IsRemoved = false;
            Products = new List<Product>();
        }

        public void Edit(string name, string slug, string keywords, string metaDescription, int categoryId)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
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
