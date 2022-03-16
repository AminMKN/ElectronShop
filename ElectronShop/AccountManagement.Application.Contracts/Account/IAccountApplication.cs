using _01_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        bool IsAdmin();
        void SignOut();
        OperationResult SignIn(SignInAccount command);
        OperationResult SignUp(SignUpAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ResetPassword(ResetPassword command);
        OperationResult ConfirmEmail(string userName, string token);
        Task<OperationResult> ForgotPassword(ForgotPassword command);
        Task<OperationResult> SendEmailConfirmation();
        Task<AccountViewModel> GetCurrentAccountInfo();
        EditAccount GetDetails(int id);
        Task<List<AccountViewModel>> GetAccounts();
        Task<List<AccountViewModel>> Search(AccountSearchModel searchModel);
    }
}
