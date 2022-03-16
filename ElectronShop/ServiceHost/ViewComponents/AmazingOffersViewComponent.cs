using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.ViewComponents
{
    public class AmazingOffersViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public AmazingOffersViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var amazingOffers = await _productQuery.GetAmazings(AmazingPosition.AmazingOffer);
            return View(amazingOffers);
        }
    }
}
