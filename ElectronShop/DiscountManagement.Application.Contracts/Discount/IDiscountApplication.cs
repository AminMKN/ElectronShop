using _01_Framework.Application;

namespace DiscountManagement.Application.Contracts.Discount
{
    public interface IDiscountApplication
    {
        OperationResult Define(DefineDiscount command);
        OperationResult Edit(EditDiscount command);
        EditDiscount GetDetails(int id);
        Task<List<DiscountViewModel>> Search(DiscountSearchModel searchModel);
    }
}