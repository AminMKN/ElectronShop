namespace ShopManagement.Application.Contracts.Order
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }
        public int DiscountRate { get; set; }
        public double Price { get; set; }
        public string Product { get; set; }
    }
}
