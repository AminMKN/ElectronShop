using _01_Framework.Application;
using _01_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.Discount;
using DiscountManagement.Domain.DiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class DiscountRepository : RepositoryBase<int, Discount>, IDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public DiscountRepository(DiscountContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }

        public EditDiscount GetDetails(int id)
        {
            return _context.Discounts.Select(x => new EditDiscount()
            {
                Id = x.Id,
                Reason = x.Reason,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi()
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<DiscountViewModel>> Search(DiscountSearchModel searchModel)
        {
            var products = await _shopContext.Products.Select(x => new { x.Id, x.Name }).AsNoTracking().ToListAsync();
            var query = _context.Discounts.Select(x => new DiscountViewModel()
            {
                Id = x.Id,
                Reason = x.Reason,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi(),
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            discounts.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return discounts;
        }
    }
}
