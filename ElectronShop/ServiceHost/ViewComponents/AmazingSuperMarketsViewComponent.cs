using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.ViewComponents
{
    public class AmazingSuperMarketsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public AmazingSuperMarketsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var amazingSuperMarkets = await _productQuery.GetAmazings(AmazingPosition.AmazingSuperMarket);
            return View(amazingSuperMarkets);
        }
    }
}
