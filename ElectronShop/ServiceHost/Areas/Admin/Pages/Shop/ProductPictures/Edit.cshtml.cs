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
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public SelectList Products;
        public EditProductPicture Command;
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;

        public EditModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (ClaimChecker.CheckShopManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                Command = _productPictureApplication.GetDetails(id);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(EditProductPicture command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet(command.Id);
                return Page();
            }

            var result = _productPictureApplication.Edit(command);
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
