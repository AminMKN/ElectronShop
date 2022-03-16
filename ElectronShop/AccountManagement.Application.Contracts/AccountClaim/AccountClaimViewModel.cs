namespace AccountManagement.Application.Contracts.AccountClaim
{
    public class AccountClaimViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountFullName { get; set; }
        public string AccountUserName { get; set; }
        public string CreationDate { get; set; }
    }
}
