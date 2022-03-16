using _01_Framework.Application;
using _01_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Amazing;
using ShopManagement.Domain.AmazingAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class AmazingRepository : RepositoryBase<int, Amazing>, IAmazingRepository
    {
        private readonly ShopContext _context;

        public AmazingRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditAmazing GetDetails(int id)
        {
            return _context.Amazings.Select(x => new EditAmazing()
            {
                Id = x.Id,
                Position = x.Position,
                ProductId = x.ProductId,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi()
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<AmazingViewModel>> Search(AmazingSearchModel searchModel)
        {
            var query = _context.Amazings.Select(x => new AmazingViewModel()
            {
                Id = x.Id,
                Position = x.Position,
                ProductId = x.ProductId,
                Product = x.Product.Name,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.Position != 0)
                query = query.Where(x => x.Position == searchModel.Position);

            return await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
    }
}
