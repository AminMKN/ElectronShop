using _02_ElectronShopQuery.Contracts.Product;

namespace _02_ElectronShopQuery.Query
{
    public class QueryHelper
    {
        public static void CalculateDiscount(int discountRate, double price, ProductQueryModel product)
        {
            product.DiscountRate = discountRate;
            product.HasDiscount = discountRate > 0;
            var discountAmount = Math.Round(price * discountRate / 100);
            product.PriceWithDiscount = price - discountAmount;
        }

        public static void CalculatePrice(double price, ProductQueryModel product)
        {
            product.Price = price;
        }
    }
}