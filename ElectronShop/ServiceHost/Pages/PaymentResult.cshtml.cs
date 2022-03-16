using _01_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class PaymentResultModel : PageModel
    {
        public PaymentResult Result { get; set; }

        public void OnGet(PaymentResult result)
        {
            Result = result;
        }
    }
}
