using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductCategories
{
    [Authorize]
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public EditProductCategory Command;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public EditModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public IActionResult OnGet(int id)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                Command = _productCategoryApplication.GetDetails(id);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnPost(EditProductCategory command)
        {
            if (!ModelState.IsValid)
            {
                OnGet(command.Id);
                return Page();
            }

            var result = _productCategoryApplication.Edit(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            OnGet(command.Id);
            return Page();
        }
    }
}
