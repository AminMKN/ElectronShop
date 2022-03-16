using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<int, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public Product GetProductWithProductCategory(int id)
        {
            return _context.Products
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductSubCategory)
                .FirstOrDefault(x => x.Id == id);
        }

        public EditProduct GetDetails(int id)
        {
            return _context.Products.Select(x => new EditProduct()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                Keywords = x.Keywords,
                Property = x.Property,
                CategoryId = x.CategoryId,
                PictureAlt = x.PictureAlt,
                Information = x.Information,
                Description = x.Description,
                PictureTitle = x.PictureTitle,
                SubCategoryId = x.SubCategoryId,
                MetaDescription = x.MetaDescription
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            return await _context.Products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductViewModel>> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Picture = x.Picture,
                IsRemoved = x.IsRemoved,
                CategoryId = x.CategoryId,
                SubCategoryId = x.SubCategoryId,
                Category = x.ProductCategory.Name,
                SubCategory = x.ProductSubCategory.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            if (searchModel.SubCategoryId != 0)
                query = query.Where(x => x.SubCategoryId == searchModel.SubCategoryId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
