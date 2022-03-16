using _02_ElectronShopQuery.Contracts.Product;
using _02_ElectronShopQuery.Contracts.ProductCategory;
using _02_ElectronShopQuery.Contracts.ProductSubCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductSubCategoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _02_ElectronShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly DiscountContext _discountContext;
        private readonly InventoryContext _inventoryContext;

        public ProductCategoryQuery(ShopContext context, DiscountContext discountContext, InventoryContext inventoryContext)
        {
            _context = context;
            _discountContext = discountContext;
            _inventoryContext = inventoryContext;
        }

        public async Task<List<ProductCategoryQueryModel>> GetProductCategories()
        {
            return await _context.ProductCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductSubCategories = MapProductSubCategories(x.ProductSubCategories)
                }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<ProductCategoryQueryModel> GetProductsWithProductCategory(string slug)
        {
            var inventory = await _inventoryContext.Inventory
             .Select(x => new { x.ProductId, x.Price }).AsNoTracking().ToListAsync();

            var discount = await _discountContext.Discounts
                   .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                   .Select(x => new { x.ProductId, x.DiscountRate }).AsNoTracking().ToListAsync();

            var category = await _context.ProductCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Products = MapProducts(x.Products)
                }).OrderByDescending(x => x.Id).AsNoTracking().FirstOrDefaultAsync(x => x.Slug == slug);

            foreach (var product in category.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    QueryHelper.CalculatePrice(productInventory.Price, product);
                    var productDiscount = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        QueryHelper.CalculateDiscount(productDiscount.DiscountRate, productInventory.Price, product);
                    }
                }
            }

            return category;
        }

        private static List<ProductQueryModel> MapProducts(IEnumerable<Product> products)
        {
            return products
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).OrderByDescending(x => x.Id).ToList();
        }

        private static List<ProductSubCategoryQueryModel> MapProductSubCategories(IEnumerable<ProductSubCategory> productSubCategories)
        {
            return productSubCategories
                .Where(x => !x.IsRemoved)
                .Select(x => new ProductSubCategoryQueryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug
                }).OrderByDescending(x => x.Id).ToList();
        }
    }
}
