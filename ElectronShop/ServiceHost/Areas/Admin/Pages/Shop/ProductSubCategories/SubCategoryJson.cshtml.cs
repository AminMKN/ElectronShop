using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductSubCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductSubCategories
{
    [Authorize]
    public class SubCategoryJsonModel : PageModel
    {
        private readonly IProductSubCategoryApplication _productSubCategoryApplication;

        public SubCategoryJsonModel(IProductSubCategoryApplication productSubCategoryApplication)
        {
            _productSubCategoryApplication = productSubCategoryApplication;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                var productSubCategoriesJson = await _productSubCategoryApplication.GetProductSubCategoriesJson(id);
                return new JsonResult(productSubCategoriesJson);
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }
    }
}
