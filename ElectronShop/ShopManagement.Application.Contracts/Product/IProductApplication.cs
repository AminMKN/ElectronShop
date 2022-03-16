using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        OperationResult Remove(int id);
        OperationResult Restore(int id);
        EditProduct GetDetails(int id);
        Task<List<ProductViewModel>> GetProducts();
        Task<List<ProductViewModel>> Search(ProductSearchModel searchModel);
    }
}
