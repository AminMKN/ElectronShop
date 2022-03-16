using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using _01_Framework.Application.Email;
using _01_Framework.Application.PasswordHasher;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IAuthHelper _authHelper;
        private readonly IEmailSender _emailSender;
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAccountRepository _accountRepository;

        public AccountApplication(LinkGenerator linkGenerator, IAuthHelper authHelper, IEmailSender emailSender, IFileUploader fileUploader, IPasswordHasher passwordHasher, IAccountRepository accountRepository, IHttpContextAccessor contextAccessor)
        {
            _linkGenerator = linkGenerator;
            _authHelper = authHelper;
            _emailSender = emailSender;
            _fileUploader = fileUploader;
            _passwordHasher = passwordHasher;
            _contextAccessor = contextAccessor;
            _accountRepository = accountRepository;
        }

        public bool IsAdmin()
        {
            var accountUserName = _authHelper.GetCurrentAccountUserName();
            var account = _accountRepository.GetAccountByUserName(accountUserName);
            if (account == null)
                return false;

            if (account.AccountClaims.Any(x => x.AccountId == account.Id))
                return true;

            return false;
        }

        public void SignOut()
        {
            _authHelper.SignOut();
        }

        public OperationResult SignIn(SignInAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByUserName(command.UserName);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var password = _passwordHasher.Check(account.Password, command.Password);
            if (!password.Verified)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var shopManagement = account.AccountClaims.Select(x => x.ShopManagement).FirstOrDefault();
            var inventoryManagement = account.AccountClaims.Select(x => x.InventoryManagement).FirstOrDefault();
            var commentManagement = account.AccountClaims.Select(x => x.CommentManagement).FirstOrDefault();
            var discountManagement = account.AccountClaims.Select(x => x.DiscountManagement).FirstOrDefault();
            var accountManagement = account.AccountClaims.Select(x => x.AccountManagement).FirstOrDefault();

            var authViewModel = new AuthViewModel(account.Id, account.FullName, account.UserName, account.Email, account.PhoneNumber, command.RememberMe,
              shopManagement, inventoryManagement, commentManagement, discountManagement, accountManagement);

            _authHelper.SignIn(authViewModel);

            return operation.Success();
        }

        public OperationResult SignUp(SignUpAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exists(x => x.UserName == command.UserName))
                return operation.Failed(ApplicationMessages.DuplicatedUserName);

            if (_accountRepository.Exists(x => x.Email == command.Email))
                return operation.Failed(ApplicationMessages.DuplicatedEmail);

            var token = CodeGenerator.GenerateToken();
            var password = _passwordHasher.Hash(command.Password);
            var account = new Account(command.FullName, command.UserName, command.Email, command.PhoneNumber, password, token);

            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_accountRepository.Exists(x => x.UserName == command.UserName && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedUserName);

            if (_accountRepository.Exists(x => x.Email == command.Email && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedEmail);

            var profilePhotoPath = $"{"Users"}/{command.UserName}";
            var profilePhoto = _fileUploader.Upload(command.ProfilePhoto, profilePhotoPath);
            account.Edit(command.FullName, command.UserName, command.Email, command.PhoneNumber, profilePhoto);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult ResetPassword(ResetPassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByEmail(command.Email);
            if (account != null && command.Email != null && command.Token == account.Token)
            {
                var password = _passwordHasher.Hash(command.Password);
                account.ChangePassword(password);
                _accountRepository.SaveChanges();
                return operation.Success();
            }

            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        public OperationResult ConfirmEmail(string userName, string token)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByUserName(userName);
            if (account != null && userName != null && token == account.Token)
            {
                account.ConfirmEmail(account.Token);
                _accountRepository.SaveChanges();
                return operation.Success();
            }

            return operation.Failed(ApplicationMessages.RecordNotFound);
        }

        public async Task<OperationResult> ForgotPassword(ForgotPassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetAccountByEmail(command.Email);
            if (account != null)
            {
                var emailMessage = _linkGenerator.GetUriByPage(_contextAccessor.HttpContext, "/Account/ResetPassword", "",
                     new { email = account.Email, token = account.Token }, _contextAccessor.HttpContext.Request.Scheme);

                await _emailSender.SendEmail(account.Email, "الکترون شاپ-تغییر کلمه عبور",
                    $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>تغییر کلمه عبور</a>", true);
            }

            return operation.Success(ApplicationMessages.ForgotPasswordEmailSend);
        }

        public async Task<OperationResult> SendEmailConfirmation()
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(_authHelper.GetCurrentAccountId());
            if (account != null)
            {
                var emailMessage = _linkGenerator.GetUriByPage(_contextAccessor.HttpContext, "/Index", "ConfirmEmail",
                    new { userName = account.UserName, token = account.Token }, _contextAccessor.HttpContext.Request.Scheme);

                await _emailSender.SendEmail(account.Email, "الکترون شاپ-فعال سازی",
                    $"<a href='{emailMessage}' style='display: block; width: 120px; height: 25px; background: #9dcc1b; padding: 10px; text-align: center; border-radius: 50px; color: black; font-weight: bold; line-height: 25px;'>فعال سازی</a>", true);
            }

            return operation.Success(ApplicationMessages.SentEmailConfirmation);
        }

        public async Task<AccountViewModel> GetCurrentAccountInfo()
        {
            return await _accountRepository.GetCurrentAccountInfo();
        }

        public EditAccount GetDetails(int id)
        {
            return _accountRepository.GetDetails(id);
        }

        public async Task<List<AccountViewModel>> GetAccounts()
        {
            return await _accountRepository.GetAccounts();
        }

        public async Task<List<AccountViewModel>> Search(AccountSearchModel searchModel)
        {
            return await _accountRepository.Search(searchModel);
        }
    }
}