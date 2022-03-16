using _01_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<int, ProductPicture>
    {
        EditProductPicture GetDetails(int id);
        ProductPicture GetProductPictureWithProductAndCategory(int id);
        Task<List<ProductPictureViewModel>> Search(ProductPictureSearchModel searchModel);
    }
}
