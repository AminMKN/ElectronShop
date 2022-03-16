using _01_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.AccountClaim
{
    public class CreateAccountClaim
    {
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public int AccountId { get; set; }
        public bool ShopManagement { get; set; }
        public bool InventoryManagement { get; set; }
        public bool DiscountManagement { get; set; }
        public bool CommentManagement { get; set; }
        public bool AccountManagement { get; set; }
    }
}
