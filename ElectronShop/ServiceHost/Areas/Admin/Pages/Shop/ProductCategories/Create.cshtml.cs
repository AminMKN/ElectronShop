using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductCategories
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public CreateProductCategory Command;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public CreateModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public IActionResult OnGet()
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnPost(CreateProductCategory command)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = _productCategoryApplication.Create(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            return Page();
        }
    }
}
