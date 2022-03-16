using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository, IAuthHelper authHelper)
        {
            _inventoryRepository = inventoryRepository;
            _authHelper = authHelper;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var inventory = new Inventory(command.ProductId, command.Price);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.Price);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var operatorId = _authHelper.GetCurrentAccountId();
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (inventory.CalculateCurrentCount() < command.Count)
                return operation.Failed(ApplicationMessages.TheCountIsMoreTheInventory);

            var operatorId = _authHelper.GetCurrentAccountId();
            inventory.Reduce(command.Count, operatorId, command.Description, 0);
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            var operation = new OperationResult();
            var operatorId = _authHelper.GetCurrentAccountId();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetInventoryWithProduct(item.ProductId);
                if (inventory != null)
                    inventory.Reduce(item.Count, operatorId, item.Description, item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Success();
        }

        public EditInventory GetDetails(int id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public async Task<List<InventoryOperationViewModel>> GetOperationLog(int id)
        {
            return await _inventoryRepository.GetOperationLog(id);
        }

        public async Task<List<InventoryViewModel>> Search(InventorySearchModel searchModel)
        {
            return await _inventoryRepository.Search(searchModel);
        }
    }
}