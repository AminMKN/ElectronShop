using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<int, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;

        public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public Inventory GetInventoryWithProduct(int productId)
        {
            return _context.Inventory
                      .FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(int id)
        {
            return _context.Inventory.Select(x => new EditInventory()
            {
                Id = x.Id,
                Price = x.Price,
                ProductId = x.ProductId
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<InventoryOperationViewModel>> GetOperationLog(int id)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(x => x.Id == id);
            var operations = inventory.InventoryOperations.Select(x => new InventoryOperationViewModel()
            {
                Id = x.Id,
                Count = x.Count,
                Operation = x.Operation,
                OperatorId = x.OperatorId,
                Description = x.Description,
                CurrentCount = x.CurrentCount,
                OperationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).ToList();

            foreach (var operation in operations)
            {
                var account = await _accountContext.Accounts.FirstOrDefaultAsync(x => x.Id == operation.OperatorId);
                operation.Operator = account.FullName;
            }

            return operations;
        }

        public async Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel)
        {
            var products = await _shopContext.Products.Select(x => new { x.Id, x.Name }).AsNoTracking().ToListAsync();
            var query = _context.Inventory.Select(x => new InventoryViewModel()
            {
                Id = x.Id,
                Price = x.Price,
                InStock = x.InStock,
                ProductId = x.ProductId,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount()
            });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);

            var inventory = await query.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            inventory.ForEach(item => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

            return inventory;
        }
    }
}