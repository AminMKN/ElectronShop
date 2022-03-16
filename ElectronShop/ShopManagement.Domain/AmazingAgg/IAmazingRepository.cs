using _01_Framework.Domain;
using ShopManagement.Application.Contracts.Amazing;

namespace ShopManagement.Domain.AmazingAgg
{
    public interface IAmazingRepository : IRepository<int, Amazing>
    {
        EditAmazing GetDetails(int id);
        Task<List<AmazingViewModel>> Search(AmazingSearchModel searchModel);
    }
}