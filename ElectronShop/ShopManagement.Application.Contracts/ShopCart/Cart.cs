namespace ShopManagement.Application.Contracts.ShopCart
{
    public class Cart
    {
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public string PhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public List<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public void Add(CartItem cartItem)
        {
            CartItems.Add(cartItem);
            TotalAmount += cartItem.TotalAmount;
            DiscountAmount += cartItem.DiscountAmount;
            PayAmount += cartItem.PayAmount;
        }
    }
}
