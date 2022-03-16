using System.ComponentModel.DataAnnotations;
using _01_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public class ForgotPassword
    {
        [EmailAddress(ErrorMessage = ValidationMessages.EmailIsNotValid)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Email { get; set; }
    }
}