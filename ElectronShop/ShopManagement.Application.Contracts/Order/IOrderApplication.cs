using ShopManagement.Application.Contracts.ShopCart;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        void Cancel(int id);
        double GetAmount(int id);
        int PlaceOrder(Cart cart);
        string PaymentSuccess(int orderId, int refId);
        Task<List<OrderItemViewModel>> GetOrderItems(int id);
        Task<List<OrderViewModel>> GetCurrentAccountOrders(int accountId);
        Task<List<OrderViewModel>> Search(OrderSearchModel searchModel);
    }
}
