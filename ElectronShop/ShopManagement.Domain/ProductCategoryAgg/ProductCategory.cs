using _01_Framework.Domain;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductSubCategoryAgg;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public bool IsRemoved { get; private set; }
        public List<Product> Products { get; private set; }
        public List<ProductSubCategory> ProductSubCategories { get; private set; }

        public ProductCategory(string name, string slug, string keywords, string metaDescription)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            IsRemoved = false;
            Products = new List<Product>();
            ProductSubCategories = new List<ProductSubCategory>();
        }

        public void Edit(string name, string slug, string keywords, string metaDescription)
        {
            Name = name;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
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
