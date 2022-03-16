using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUploader fileUploader, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithProductCategory(command.ProductId);
            if (product == null || product.ProductCategory == null || product.ProductSubCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var picturePath = $"{product.ProductCategory.Slug}/{product.ProductSubCategory.Slug}/{product.Slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            var productPicture = new ProductPicture(picture, command.PictureAlt, command.PictureTitle, command.ProductId);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetProductPictureWithProductAndCategory(command.Id);
            if (productPicture == null || productPicture.Product == null || productPicture.Product.ProductCategory == null || productPicture.Product.ProductSubCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var picturePath = $"{productPicture.Product.ProductCategory.Slug}/{productPicture.Product.ProductSubCategory.Slug}/{productPicture.Product.Slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            productPicture.Edit(picture, command.PictureAlt, command.PictureTitle, command.ProductId);
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Restore();
            _productPictureRepository.SaveChanges();
            return operation.Success();
        }

        public EditProductPicture GetDetails(int id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public async Task<List<ProductPictureViewModel>> Search(ProductPictureSearchModel searchModel)
        {
            return await _productPictureRepository.Search(searchModel);
        }
    }
}