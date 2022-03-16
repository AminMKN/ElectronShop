using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Amazing;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Admin.Pages.Shop.Amazings
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public SelectList Products;
        public AmazingSearchModel SearchModel;
        public List<AmazingViewModel> Amazings;
        private readonly IProductApplication _productApplication;
        private readonly IAmazingApplication _amazingApplication;

        public IndexModel(IProductApplication productApplication, IAmazingApplication amazingApplication)
        {
            _productApplication = productApplication;
            _amazingApplication = amazingApplication;
        }

        public async Task<IActionResult> OnGet(AmazingSearchModel searchModel)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                Amazings = await _amazingApplication.Search(searchModel);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }
    }
}
