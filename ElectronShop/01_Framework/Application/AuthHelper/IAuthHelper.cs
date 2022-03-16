namespace _01_Framework.Application.AuthHelper
{
    public interface IAuthHelper
    {
        int GetCurrentAccountId();
        string GetCurrentAccountFullName();
        string GetCurrentAccountUserName();
        string GetCurrentAccountEmail();
        string GetCurrentAccountPhoneNumber();
        void SignIn(AuthViewModel authViewModel);
        void SignOut();
        bool IsAuthenticated();
    }
}
