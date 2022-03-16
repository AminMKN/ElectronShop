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
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList Products;
        public EditDiscount Command;
        private readonly IProductApplication _productApplication;
        private readonly IDiscountApplication _discountApplication;

        public EditModel(IDiscountApplication discountApplication, IProductApplication productApplication)
        {
            _discountApplication = discountApplication;
            _productApplication = productApplication;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (ClaimChecker.CheckDiscountManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                Command = _discountApplication.GetDetails(id);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(EditDiscount command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet(command.Id);
                return Page();
            }

            var result = _discountApplication.Edit(command);
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
