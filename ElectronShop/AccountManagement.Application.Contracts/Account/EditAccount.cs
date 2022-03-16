using _01_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount
    {
        public int Id { get; set; }

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

        public IFormFile ProfilePhoto { get; set; }
    }
}