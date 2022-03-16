namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventoryOperationViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int CurrentCount { get; set; }
        public int OperatorId { get; set; }
        public string Operator { get; set; }
        public string OperationDate { get; set; }
        public string Description { get; set; }
        public bool Operation { get; set; }
    }
}