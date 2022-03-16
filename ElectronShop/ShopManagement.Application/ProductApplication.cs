using _01_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductSubCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductSubCategoryRepository _productSubCategoryRepository;

        public ProductApplication(IFileUploader fileUploader, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IProductSubCategoryRepository productSubCategoryRepository)
        {
            _fileUploader = fileUploader;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productSubCategoryRepository = productSubCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var categorySlug = _productCategoryRepository.GetProductCategorySlug(command.CategoryId);
            var subCategorySlug = _productSubCategoryRepository.GetProductSubCategorySlug(command.SubCategoryId);
            var picturePath = $"{categorySlug}/{subCategorySlug}/{slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            var product = new Product(command.Name, slug, command.Code, command.Information, command.Property, command.Description,
                picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, command.CategoryId, command.SubCategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithProductCategory(command.Id);
            if (product == null || product.ProductCategory == null || product.ProductSubCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var picturePath = $"{product.ProductCategory.Slug}/{product.ProductSubCategory.Slug}/{slug}";
            var picture = _fileUploader.Upload(command.Picture, picturePath);
            product.Edit(command.Name, slug, command.Code, command.Information, command.Property, command.Description,
                picture, command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, command.CategoryId, command.SubCategoryId);
            _productRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.Remove();
            _productRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            product.Restore();
            _productRepository.SaveChanges();
            return operation.Success();
        }

        public EditProduct GetDetails(int id)
        {
            return _productRepository.GetDetails(id);
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<List<ProductViewModel>> Search(ProductSearchModel searchModel)
        {
            return await _productRepository.Search(searchModel);
        }
    }
}
