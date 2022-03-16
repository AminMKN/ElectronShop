using _01_Framework.Application.AuthHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Admin.Pages.Shop.ProductPictures
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList Products;
        public CreateProductPicture Command;
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;

        public CreateModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
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

        public async Task<IActionResult> OnPost(CreateProductPicture command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            var result = _productPictureApplication.Create(command);
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
