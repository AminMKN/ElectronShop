using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<int, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(int id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetProductPictureWithProductAndCategory(int id)
        {
            return _context.ProductPictures
                .Include(x => x.Product)
                .ThenInclude(x => x.ProductCategory)
                .ThenInclude(x => x.ProductSubCategories)
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<ProductPictureViewModel>> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Select(x => new ProductPictureViewModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved,
                Product = x.Product.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });

            query = query.Where(x => x.IsRemoved == searchModel.IsRemoved);

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
