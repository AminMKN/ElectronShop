using _01_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<int, Inventory>
    {
        Inventory GetInventoryWithProduct(int productId);
        EditInventory GetDetails(int id);
        Task<List<InventoryOperationViewModel>> GetOperationLog(int id);
        Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel);
    }
}
