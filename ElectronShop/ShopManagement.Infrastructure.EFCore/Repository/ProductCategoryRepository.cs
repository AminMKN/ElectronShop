using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<int, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public string GetProductCategorySlug(int id)
        {
            return _context.ProductCategories
                .Select(x => new { x.Id, x.Slug }).AsNoTracking().FirstOrDefault(x => x.Id == id)?.Slug;
        }

        public EditProductCategory GetDetails(int id)
        {
            return _context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductCategoryViewModel>> GetProductCategories()
        {
            return await _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductCategoryViewModel>> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToFarsi()
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
