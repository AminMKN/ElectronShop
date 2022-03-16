using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class ReduceInventory
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int Count { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }

        public ReduceInventory(int productId, int orderId, int count, string description)
        {
            ProductId = productId;
            OrderId = orderId;
            Count = count;
            Description = description;
        }

        public ReduceInventory()
        {

        }
    }
}