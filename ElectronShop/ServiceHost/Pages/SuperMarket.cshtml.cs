using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.Pages
{
    public class SuperMarketModel : PageModel
    {
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _productQuery;

        public SuperMarketModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task OnGet()
        {
            Products = await _productQuery.GetAmazings(AmazingPosition.AmazingSuperMarket);
        }
    }
}
