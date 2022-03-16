using _01_Framework.Domain;
using AccountManagement.Domain.AccountClaimAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Password { get; private set; }
        public string Token { get; private set; }
        public string ProfilePhoto { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public List<AccountClaim> AccountClaims { get; private set; }

        public Account(string fullName, string userName, string email, string phoneNumber, string password, string token)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Token = token;
            EmailConfirmed = false;
        }

        public Account(int id, string fullName, string userName, string email, string phoneNumber,
            string password, string token)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Token = token;
            EmailConfirmed = true;
        }

        public void Edit(string fullName, string userName, string email, string phoneNumber, string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void ConfirmEmail(string token)
        {
            Token = token;
            EmailConfirmed = true;
        }
    }
}
