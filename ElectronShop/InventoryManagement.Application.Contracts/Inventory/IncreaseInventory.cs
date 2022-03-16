using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class IncreaseInventory
    {
        public int InventoryId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int Count { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }
    }
}
