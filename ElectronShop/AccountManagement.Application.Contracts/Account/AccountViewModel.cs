namespace AccountManagement.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePhoto { get; set; }
        public string CreationDate { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
