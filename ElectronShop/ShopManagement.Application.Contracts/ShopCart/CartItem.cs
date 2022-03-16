namespace ShopManagement.Application.Contracts.ShopCart
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int DiscountRate { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public bool InStock { get; set; }

        public void CalculateTotalItemPrice()
        {
            TotalAmount = Price * Count;
        }
    }
}