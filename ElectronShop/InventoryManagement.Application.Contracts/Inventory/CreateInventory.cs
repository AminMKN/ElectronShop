using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public double Price { get; set; }
    }
}