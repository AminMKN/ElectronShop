using _01_Framework.Application.AuthHelper;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Inventory
{
    [Authorize]
    public class OperationLogModel : PageModel
    {
        public List<InventoryOperationViewModel> InventoryOperations;
        private readonly IInventoryApplication _inventoryApplication;

        public OperationLogModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if (ClaimChecker.CheckInventoryManagementClaim())
            {
                InventoryOperations = await _inventoryApplication.GetOperationLog(id);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }
    }
}
