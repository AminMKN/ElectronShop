namespace _01_Framework.Application.ZarinPal
{
    public class PaymentRequest
    {
        public int Amount { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CallbackURL { get; set; }
        public string Description { get; set; }
        public string MerchantID { get; set; }
    }
}
