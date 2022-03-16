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
    public class IndexModel : PageModel
    {
        public SelectList Products;
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public async Task<IActionResult> OnGet(InventorySearchModel searchModel)
        {
            if (ClaimChecker.CheckInventoryManagementClaim())
            {
                Products = new SelectList(await _productApplication.GetProducts(), "Id", "Name");
                Inventory = await _inventoryApplication.Search(searchModel);
                return Page();
            }
            
            return RedirectToPage("/Account/AccessDenied");
        }
    }
}
