namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CurrentCount { get; set; }
        public double Price { get; set; }
        public string Product { get; set; }
        public string CreationDate { get; set; }
        public bool InStock { get; set; }
    }
}