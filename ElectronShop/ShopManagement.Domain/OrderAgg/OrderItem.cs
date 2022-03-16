using _01_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class OrderItem : EntityBase
    {
        public int ProductId { get; private set; }
        public int OrderId { get; private set; }
        public int Count { get; private set; }
        public int DiscountRate { get; private set; }
        public double Price { get; private set; }
        public Order Order { get; private set; }

        public OrderItem(int productId, int count, int discountRate, double price)
        {
            ProductId = productId;
            Count = count;
            DiscountRate = discountRate;
            Price = price;
        }
    }
}