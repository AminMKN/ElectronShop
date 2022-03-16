using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class ResetPassword
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MinLength(6, ErrorMessage = ValidationMessages.MinPasswordLength)]
        [MaxLength(12, ErrorMessage = ValidationMessages.MaxPasswordLength)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Compare(nameof(Password), ErrorMessage = ValidationMessages.PasswordAndRePasswordDoNotMatch)]
        public string RePassword { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Token { get; set; }
    }
}