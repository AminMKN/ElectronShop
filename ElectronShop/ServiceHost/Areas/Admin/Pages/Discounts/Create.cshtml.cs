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
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList Products;
        public DefineDiscount Command;
        private readonly IProductApplication _productApplication;
        private readonly IDiscountApplication _discountApplication;

        public CreateModel(IDiscountApplication discountApplication, IProductApplication productApplication)
        {
            _discountApplication = discountApplication;
            _productApplication = productApplication;
        }

        public async Task<IActionResult> OnGet()
        {
            if (ClaimChecker.CheckDiscountManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(DefineDiscount command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            var result = _discountApplication.Define(command);
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
