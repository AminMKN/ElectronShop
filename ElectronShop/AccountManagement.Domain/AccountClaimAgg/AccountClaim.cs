using _01_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.AccountClaimAgg
{
    public class AccountClaim : EntityBase
    {
        public int AccountId { get; private set; }
        public bool ShopManagement { get; private set; }
        public bool InventoryManagement { get; private set; }
        public bool DiscountManagement { get; private set; }
        public bool CommentManagement { get; private set; }
        public bool AccountManagement { get; private set; }
        public Account Account { get; private set; }

        public AccountClaim(int accountId, bool shopManagement, bool inventoryManagement, bool discountManagement,
            bool commentManagement, bool accountManagement)
        {
            AccountId = accountId;
            ShopManagement = shopManagement;
            InventoryManagement = inventoryManagement;
            DiscountManagement = discountManagement;
            CommentManagement = commentManagement;
            AccountManagement = accountManagement;
        }

        public AccountClaim(int id, int accountId, bool shopManagement, bool inventoryManagement, bool discountManagement,
            bool commentManagement, bool accountManagement)
        {
            Id = id;
            AccountId = accountId;
            ShopManagement = shopManagement;
            InventoryManagement = inventoryManagement;
            DiscountManagement = discountManagement;
            CommentManagement = commentManagement;
            AccountManagement = accountManagement;
        }

        public void Edit(int accountId, bool shopManagement, bool inventoryManagement, bool discountManagement,
            bool commentManagement, bool accountManagement)
        {
            AccountId = accountId;
            ShopManagement = shopManagement;
            InventoryManagement = inventoryManagement;
            DiscountManagement = discountManagement;
            CommentManagement = commentManagement;
            AccountManagement = accountManagement;
        }
    }
}
