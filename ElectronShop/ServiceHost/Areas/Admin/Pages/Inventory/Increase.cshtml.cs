using _01_Framework.Application.AuthHelper;
using InventoryManagement.Application.Contracts.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Inventory
{
    [Authorize]
    public class IncreaseModel : PageModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IncreaseInventory Command;
        private readonly IInventoryApplication _inventoryApplication;

        public IncreaseModel(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public IActionResult OnGet(int id)
        {
            if (ClaimChecker.CheckInventoryManagementClaim())
            {
                Command = new IncreaseInventory()
                {
                    InventoryId = id
                };
                return Page();
            }
           
            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnPost(IncreaseInventory command)
        {
            if (!ModelState.IsValid)
            {
                OnGet(command.InventoryId);
                return Page();
            }

            var result = _inventoryApplication.Increase(command);
            if (result.IsSuccess)
                IsSuccess = true;
            else
                IsSuccess = false;

            Message = result.Message;
            OnGet(command.InventoryId);
            return Page();
        }
    }
}
