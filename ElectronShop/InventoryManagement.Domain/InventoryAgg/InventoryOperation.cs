using _01_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation : EntityBase
    {
        public int Count { get; private set; }
        public int CurrentCount { get; private set; }
        public int OperatorId { get; private set; }
        public string Description { get; private set; }
        public int OrderId { get; private set; }
        public int InventoryId { get; private set; }
        public bool Operation { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(int count, int currentCount, int operatorId, string description, int orderId,
            int inventoryId, bool operation)
        {
            Count = count;
            CurrentCount = currentCount;
            OperatorId = operatorId;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
            Operation = operation;
        }
    }
}
