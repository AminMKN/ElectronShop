namespace _01_Framework.Application.ZarinPal
{
    public interface IZarinPalFactory
    {
        string Prefix { get; set; }
        PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description, int orderId);
        VerificationResponse CreateVerificationRequest(string authority, string amount);
    }
}