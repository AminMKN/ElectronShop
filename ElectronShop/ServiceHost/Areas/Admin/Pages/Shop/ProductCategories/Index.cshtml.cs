using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductCategories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public ProductCategorySearchModel SearchModel;
        public List<ProductCategoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public async Task<IActionResult> OnGet(ProductCategorySearchModel searchModel)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                ProductCategories = await _productCategoryApplication.Search(searchModel);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnGetRemove(int id)
        {
            _productCategoryApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(int id)
        {
            _productCategoryApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
