using _02_ElectronShopQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productQuery.GetProducts();
            return View(products);
        }
    }
}
