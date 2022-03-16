namespace _01_Framework.Application.AuthHelper
{
    public class AuthViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool RememberMe { get; set; }
        public bool ShopManagement { get; set; }
        public bool InventoryManagement { get; set; }
        public bool CommentManagement { get; set; }
        public bool DiscountManagement { get; set; }
        public bool AccountManagement { get; set; }

        public AuthViewModel(int id, string fullName, string userName, string email, string phoneNumber, bool rememberMe,
            bool shopManagement, bool inventoryManagement, bool commentManagement, bool discountManagement, bool accountManagement)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            RememberMe = rememberMe;
            ShopManagement = shopManagement;
            InventoryManagement = inventoryManagement;
            CommentManagement = commentManagement;
            DiscountManagement = discountManagement;
            AccountManagement = accountManagement;
        }
    }
}
