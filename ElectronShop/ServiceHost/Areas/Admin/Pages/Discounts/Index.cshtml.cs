using _01_Framework.Application.AuthHelper;
using DiscountManagement.Application.Contracts.Discount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Admin.Pages.Discounts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public SelectList Products;
        public DiscountSearchModel SearchModel;
        public List<DiscountViewModel> Discounts;
        private readonly IProductApplication _productApplication;
        private readonly IDiscountApplication _discountApplication;

        public IndexModel(IDiscountApplication discountApplication, IProductApplication productApplication)
        {
            _discountApplication = discountApplication;
            _productApplication = productApplication;
        }

        public async Task<IActionResult> OnGet(DiscountSearchModel searchModel)
        {
            if (ClaimChecker.CheckDiscountManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                Discounts = await _discountApplication.Search(searchModel);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }
    }
}