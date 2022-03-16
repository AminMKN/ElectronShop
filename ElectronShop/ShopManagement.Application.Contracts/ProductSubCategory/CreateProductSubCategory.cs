using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductSubCategory
{
    public class CreateProductSubCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int CategoryId { get; set; }
    }
}
