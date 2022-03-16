using _01_Framework.Domain;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Domain.OrderAgg
{
    public interface IOrderRepository : IRepository<int, Order>
    {
        double GetAmount(int id);
        Task<List<OrderItemViewModel>> GetOrderItems(int id);
        Task<List<OrderViewModel>> GetCurrentAccountOrders(int accountId);
        Task<List<OrderViewModel>> Search(OrderSearchModel searchModel);
    }
}