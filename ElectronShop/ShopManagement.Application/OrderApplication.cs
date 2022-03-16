using _01_Framework.Application;
using _01_Framework.Application.AuthHelper;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Application.Contracts.ShopCart;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IOrderRepository _orderRepository;
        private readonly IShopInventoryAcl _shopInventoryAcl;

        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl shopInventoryAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _shopInventoryAcl = shopInventoryAcl;
        }

        public void Cancel(int id)
        {
            var order = _orderRepository.Get(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        }

        public double GetAmount(int id)
        {
            return _orderRepository.GetAmount(id);
        }

        public int PlaceOrder(Cart cart)
        {
            var accountId = _authHelper.GetCurrentAccountId();
            var phoneNumber = _authHelper.GetCurrentAccountPhoneNumber();
            var order = new Order(accountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount,
                cart.PostalCode, phoneNumber, cart.Address);

            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, cartItem.DiscountRate, cartItem.Price);
                order.Add(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public string PaymentSuccess(int orderId, int refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSuccess(refId);
            var issueTrackingNo = CodeGenerator.Generate("S");
            order.SetIssueTrackingNo(issueTrackingNo);
            if (_shopInventoryAcl.ReduceFromInventory(order.OrderItems))
            {
                _orderRepository.SaveChanges();
                return issueTrackingNo;
            }

            return null;
        }

        public async Task<List<OrderItemViewModel>> GetOrderItems(int id)
        {
            return await _orderRepository.GetOrderItems(id);
        }

        public async Task<List<OrderViewModel>> GetCurrentAccountOrders(int accountId)
        {
            return await _orderRepository.GetCurrentAccountOrders(accountId);
        }

        public async Task<List<OrderViewModel>> Search(OrderSearchModel searchModel)
        {
            return await _orderRepository.Search(searchModel);
        }
    }
}
