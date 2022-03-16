using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        OperationResult Remove(int id);
        OperationResult Restore(int id);
        EditProductPicture GetDetails(int id);
        Task<List<ProductPictureViewModel>> Search(ProductPictureSearchModel searchModel);
    }
}