using _01_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public int ProductId { get; private set; }
        public double Price { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        public Inventory(int productId, double price)
        {
            ProductId = productId;
            Price = price;
            InStock = false;
            InventoryOperations = new List<InventoryOperation>();
        }

        public void Edit(int productId, double price)
        {
            ProductId = productId;
            Price = price;
        }

        public int CalculateCurrentCount()
        {
            var plus = InventoryOperations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = InventoryOperations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(int count, int operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(count, currentCount, operatorId, description, 0, Id, true);
            InventoryOperations.Add(operation);
            InStock = currentCount > 0;
        }

        public void Reduce(int count, int operatorId, string description, int orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(count, currentCount, operatorId, description, orderId, Id, false);
            InventoryOperations.Add(operation);
            InStock = currentCount > 0;
        }
    }
}
