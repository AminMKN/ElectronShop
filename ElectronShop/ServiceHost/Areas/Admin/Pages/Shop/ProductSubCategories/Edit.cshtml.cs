using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductSubCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductSubCategories
{
    [Authorize]
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList ProductCategories;
        public EditProductSubCategory Command;
        private readonly IProductCategoryApplication _productCategoryApplication;
        private readonly IProductSubCategoryApplication _productSubCategoryApplication;

        public EditModel(IProductSubCategoryApplication productSubCategoryApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productSubCategoryApplication = productSubCategoryApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategories(), "Id", "Name");
                Command = _productSubCategoryApplication.GetDetails(id);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(EditProductSubCategory command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet(command.Id);
                return Page();
            }

            var result = _productSubCategoryApplication.Edit(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            await OnGet(command.Id);
            return Page();
        }
    }
}
