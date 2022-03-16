using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Information { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Property { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }

        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int CategoryId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int SubCategoryId { get; set; }
    }
}
