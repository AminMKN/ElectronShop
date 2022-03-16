using _01_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.AmazingAgg
{
    public class Amazing : EntityBase
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int Position { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }

        public Amazing(DateTime startDate, DateTime endDate, int position, int productId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Position = position;
            ProductId = productId;
        }

        public void Edit(DateTime startDate, DateTime endDate, int position, int productId)
        {
            StartDate = startDate;
            EndDate = endDate;
            Position = position;
            ProductId = productId;
        }
    }
}
