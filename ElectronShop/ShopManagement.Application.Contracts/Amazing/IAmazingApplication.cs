using _01_Framework.Application;

namespace ShopManagement.Application.Contracts.Amazing
{
    public interface IAmazingApplication
    {
        OperationResult Create(CreateAmazing command);
        OperationResult Edit(EditAmazing command);
        EditAmazing GetDetails(int id);
        Task<List<AmazingViewModel>> Search(AmazingSearchModel searchModel);
    }
}
