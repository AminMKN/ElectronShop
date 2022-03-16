using _01_Framework.Application;
using ShopManagement.Application.Contracts.ProductSubCategory;
using ShopManagement.Domain.ProductSubCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductSubCategoryApplication : IProductSubCategoryApplication
    {
        private readonly IProductSubCategoryRepository _productSubCategoryRepository;

        public ProductSubCategoryApplication(IProductSubCategoryRepository productSubCategoryRepository)
        {
            _productSubCategoryRepository = productSubCategoryRepository;
        }

        public OperationResult Create(CreateProductSubCategory command)
        {
            var operation = new OperationResult();
            if (_productSubCategoryRepository.Exists(x => x.Name == command.Name && x.CategoryId == command.CategoryId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var productSubCategory = new ProductSubCategory(command.Name, slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productSubCategoryRepository.Create(productSubCategory);
            _productSubCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditProductSubCategory command)
        {
            var operation = new OperationResult();
            var productSubCategory = _productSubCategoryRepository.Get(command.Id);
            if (productSubCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productSubCategoryRepository.Exists(x => x.Name == command.Name && x.CategoryId == command.CategoryId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            productSubCategory.Edit(command.Name, slug, command.Keywords, command.MetaDescription, command.CategoryId);
            _productSubCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Remove(int id)
        {
            var operation = new OperationResult();
            var productSubCategory = _productSubCategoryRepository.Get(id);
            if (productSubCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productSubCategory.Remove();
            _productSubCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Restore(int id)
        {
            var operation = new OperationResult();
            var productSubCategory = _productSubCategoryRepository.Get(id);
            if (productSubCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productSubCategory.Restore();
            _productSubCategoryRepository.SaveChanges();
            return operation.Success();
        }

        public EditProductSubCategory GetDetails(int id)
        {
            return _productSubCategoryRepository.GetDetails(id);
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategories()
        {
            return await _productSubCategoryRepository.GetProductSubCategories();
        }

        public async Task<List<ProductSubCategoryViewModel>> GetProductSubCategoriesJson(int id)
        {
            return await _productSubCategoryRepository.GetProductSubCategoriesJson(id);
        }

        public async Task<List<ProductSubCategoryViewModel>> Search(ProductSubCategorySearchModel searchModel)
        {
            return await _productSubCategoryRepository.Search(searchModel);
        }
    }
}
