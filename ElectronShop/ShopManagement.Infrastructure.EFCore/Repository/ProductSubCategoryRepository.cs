using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductSubCategory;
using ShopManagement.Domain.ProductSubCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductSubCategoryRepository : RepositoryBase<int, ProductSubCategory>, IProductSubCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductSubCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public string GetProductSubCategorySlug(int id)
        {
            return _context.ProductSubCategories
                .Select(x => new { x.Id, x.Slug }).AsNoTracking().FirstOrDefault(x => x.Id == id)?.Slug;
        }

        public EditProductSubCategory GetDetails(int id)
        {
            return _context.ProductSubCategories.Select(x => new EditProductSubCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Keywords = x.Keywords,
                CategoryId = x.CategoryId,
                MetaDescription = x.MetaDescription
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategories()
        {
            return await _context.ProductSubCategories.Select(x => new ProductSubCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesJson(int id)
        {
            return await _context.ProductSubCategories
              .Where(x => x.CategoryId == id)
              .Select(x => new ProductSubCategoryViewModel()
              {
                  Id = x.Id,
                  Name = x.Name
              }).AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductSubCategoryViewModel>> Search(ProductSubCategorySearchModel searchModel)
        {
            var query = _context.ProductSubCategories.Select(x => new ProductSubCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IsRemoved = x.IsRemoved,
                CategoryId = x.CategoryId,
                Category = x.ProductCategory.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
