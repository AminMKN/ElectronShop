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
    public class IndexModel : PageModel
    {
        public SelectList ProductCategories;
        public SelectList ProductSubCategories;
        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        private readonly IProductSubCategoryApplication _productSubCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication, IProductSubCategoryApplication productSubCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
            _productSubCategoryApplication = productSubCategoryApplication;
        }

        public async Task<IActionResult> OnGet(ProductSearchModel searchModel)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                ProductCategories = new SelectList(await _productCategoryApplication.GetProductCategories(), "Id", "Name");
                ProductSubCategories = new SelectList(await _productSubCategoryApplication.GetProductSubCategories(), "Id", "Name");
                Products = await _productApplication.Search(searchModel);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnGetRemove(int id)
        {
            _productApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(int id)
        {
            _productApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
