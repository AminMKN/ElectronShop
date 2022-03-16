using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class OrderRepository : RepositoryBase<int, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetAmount(int id)
        {
            var order = _context.Orders.Select(x => new { x.Id, x.PayAmount }).FirstOrDefault(x => x.Id == id);
            if (order != null)
                return order.PayAmount;

            return 0;
        }

        public async Task<List<OrderItemViewModel>> GetOrderItems(int id)
        {
            var products = await _context.Products.Select(x => new { x.Id, x.Name }).AsNoTracking().ToListAsync();
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.OrderItems.Select(x => new OrderItemViewModel()
            {
                Id = x.Id,
                Count = x.Count,
                Price = x.Price,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate
            }).OrderByDescending(x => x.Id).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return items;
        }

        public async Task<List<OrderViewModel>> GetCurrentAccountOrders(int accountId)
        {
            return await _context.Orders.Where(x => x.AccountId == accountId).Select(x => new OrderViewModel()
            {
                Id = x.Id,
                RefId = x.RefId,
                IsPaid = x.IsPaid,
                AccountId = x.AccountId,
                Address = x.Address,
                PayAmount = x.PayAmount,
                IsCanceled = x.IsCanceled,
                PostalCode = x.PostalCode,
                TotalAmount = x.TotalAmount,
                PhoneNumber = x.PhoneNumber,
                DiscountAmount = x.DiscountAmount,
                IssueTrackingNo = x.IssueTrackingNo,
                CreationDate = x.CreationDate.ToFarsi(),
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<List<OrderViewModel>> Search(OrderSearchModel searchModel)
        {
            var query = _context.Orders.Select(x => new OrderViewModel()
            {
                Id = x.Id,
                RefId = x.RefId,
                IsPaid = x.IsPaid,
                AccountId = x.AccountId,
                Address = x.Address,
                PayAmount = x.PayAmount,
                IsCanceled = x.IsCanceled,
                PostalCode = x.PostalCode,
                TotalAmount = x.TotalAmount,
                PhoneNumber = x.PhoneNumber,
                DiscountAmount = x.DiscountAmount,
                IssueTrackingNo = x.IssueTrackingNo,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (!string.IsNullOrWhiteSpace(searchModel.IssueTrackingNo))
                query = query.Where(x => x.IssueTrackingNo.Contains(searchModel.IssueTrackingNo));

            if (searchModel.AccountId != 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);

            var orders = query.OrderByDescending(x => x.Id).AsNoTracking().ToList();

            foreach (var order in orders)
            {
                var account = await _accountContext.Accounts.FirstOrDefaultAsync(x => x.Id == order.AccountId);
                order.FullName = account.FullName;
            }

            return orders;
        }
    }
}
