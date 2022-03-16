using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductSubCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.Products
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList ProductCategories;
        public SelectList ProductSubCategories;
        public CreateProduct Command;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        private readonly IProductSubCategoryApplication _productSubCategoryApplication;

        public CreateModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication, IProductSubCategoryApplication productSubCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
            _productSubCategoryApplication = productSubCategoryApplication;
        }

        public async Task<IActionResult> OnGet()
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategories(), "Id", "Name");
                ProductSubCategories = new SelectList(await _productSubCategoryApplication.GetProductSubCategories(), "Id", "Name");
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(CreateProduct command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            var result = _productApplication.Create(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            await OnGet();
            return Page();
        }
    }
}
