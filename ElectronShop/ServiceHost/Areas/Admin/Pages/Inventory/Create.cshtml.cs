using _01_Framework.Application.AuthHelper;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Admin.Pages.Inventory
{
    [Authorize]
    public class CreateModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public CreateInventory Command;
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public CreateModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public async Task<IActionResult> OnGet()
        {
            if (ClaimChecker.CheckInventoryManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public async Task<IActionResult> OnPost(CreateInventory command)
        {
            if (!ModelState.IsValid)
            {
                await OnGet();
                return Page();
            }

            var result = _inventoryApplication.Create(command);
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
