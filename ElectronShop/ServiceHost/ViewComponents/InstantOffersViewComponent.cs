using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.Amazing;

namespace ServiceHost.ViewComponents
{
    public class InstantOffersViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public InstantOffersViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var instantOffers = await _productQuery.GetAmazings(AmazingPosition.InstantOffer);
            return View(instantOffers);
        }
    }
}
