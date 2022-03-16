using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class DiscountModel : PageModel
    {
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _productQuery;

        public DiscountModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task OnGet()
        {
            Products = await _productQuery.GetProductsHaveDiscount();
        }
    }
}
