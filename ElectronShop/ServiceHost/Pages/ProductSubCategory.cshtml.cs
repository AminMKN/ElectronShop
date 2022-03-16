using _02_ElectronShopQuery.Contracts.ProductSubCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductSubCategoryModel : PageModel
    {
        public ProductSubCategoryQueryModel ProductSubCategory;
        private readonly IProductSubCategoryQuery _productSubCategoryQuery;

        public ProductSubCategoryModel(IProductSubCategoryQuery productSubCategoryQuery)
        {
            _productSubCategoryQuery = productSubCategoryQuery;
        }

        public async Task OnGet(string id)
        {
            ProductSubCategory = await _productSubCategoryQuery.GetProductsWithProductSubCategory(id);
        }
    }
}
