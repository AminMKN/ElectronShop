using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class SignUpAccount
    {
        [RegularExpression("^[\u0622\u0627\u0628\u067E\u062A-\u062C\u0686\u062D-\u0632\u0698\u0633-\u063A\u0641\u0642\u06A9\u06AF\u0644-\u0648\u06CC_ ]+$", ErrorMessage = ValidationMessages.PersianCharacters)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = ValidationMessages.EmailIsNotValid)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }

        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = ValidationMessages.PhoneNumberIsNotValid)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MinLength(6, ErrorMessage = ValidationMessages.MinPasswordLength)]
        [MaxLength(12, ErrorMessage = ValidationMessages.MaxPasswordLength)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Compare(nameof(Password), ErrorMessage = ValidationMessages.PasswordAndRePasswordDoNotMatch)]
        public string RePassword { get; set; }
    }
}
