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
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList Products;
        public CreateAmazing Command;
        private readonly IProductApplication _productApplication;
        private readonly IAmazingApplication _amazingApplication;

        public CreateModel(IProductApplication productApplication, IAmazingApplication amazingApplication)
        {
            _productApplication = productApplication;
            _amazingApplication = amazingApplication;
        }

        public async Task<IActionResult> OnGet()
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(CreateAmazing command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            var result = _amazingApplication.Create(command);
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
