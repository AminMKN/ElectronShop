using _01_Framework.Domain;
using DiscountManagement.Application.Contracts.Discount;

namespace DiscountManagement.Domain.DiscountAgg
{
    public interface IDiscountRepository : IRepository<int, Discount>
    {
        EditDiscount GetDetails(int id);
        Task<List<DiscountViewModel>> Search(DiscountSearchModel searchModel);
    }
}
