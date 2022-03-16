using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using _02_ElectronShopQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        [TempData]
        public bool IsSuccess { get; set; }
        public ProductQueryModel Product;
        public AddComment Command;
        private readonly IAuthHelper _authHelper;
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery, IAuthHelper authHelper, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _authHelper = authHelper;
            _commentApplication = commentApplication;
        }

        public async Task OnGet(string id)
        {
            Product = await _productQuery.GetProductDetails(id);
        }

        public IActionResult OnPost(AddComment command)
        {
            if (!_authHelper.IsAuthenticated())
                return RedirectToPage("/Account/SignIn");

            if (!ModelState.IsValid)
            {
                Message = ApplicationMessages.CommentFailed;
                IsSuccess = false;
                return RedirectToPage("/Product");
            }

            command.Type = CommentTypes.Products;
            command.AccountId = _authHelper.GetCurrentAccountId();
            var result = _commentApplication.Add(command);
            if (result.IsSuccess)
            {
                Message = ApplicationMessages.CommentSuccess;
                IsSuccess = true;
                return RedirectToPage("/Product");
            }

            Message = ApplicationMessages.CommentFailed;
            IsSuccess = false;
            return RedirectToPage("/Product");
        }
    }
}
