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
    public class IndexModel : PageModel
    {
        public SelectList ProductCategories;
        public ProductSubCategorySearchModel SearchModel;
        public List<ProductSubCategoryViewModel> ProductSubCategories;
        private readonly IProductCategoryApplication _productCategoryApplication;
        private readonly IProductSubCategoryApplication _productSubCategoryApplication;

        public IndexModel(IProductSubCategoryApplication productSubCategoryApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productSubCategoryApplication = productSubCategoryApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public async Task<IActionResult> OnGet(ProductSubCategorySearchModel searchModel)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategories(), "Id", "Name");
                ProductSubCategories = await _productSubCategoryApplication.Search(searchModel);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnGetRemove(int id)
        {
            _productSubCategoryApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(int id)
        {
            _productSubCategoryApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
