using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Amazing
{
    public class CreateAmazing
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get; set; }

        [Range(1, 3, ErrorMessage = ValidationMessages.IsRequired)]
        public int Position { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int ProductId { get; set; }
    }
}
