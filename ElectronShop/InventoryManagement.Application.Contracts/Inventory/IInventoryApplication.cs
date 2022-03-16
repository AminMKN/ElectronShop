using _01_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(ReduceInventory command);
        OperationResult Reduce(List<ReduceInventory> command);
        EditInventory GetDetails(int id);
        Task<List<InventoryOperationViewModel>> GetOperationLog(int id);
        Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel);
    }
}
