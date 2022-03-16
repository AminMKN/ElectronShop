using _01_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public int AccountId { get; private set; }
        public int RefId { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public string PostalCode { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(int accountId, double totalAmount, double discountAmount, double payAmount,
            string postalCode, string phoneNumber, string address)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Address = address;
            RefId = 0;
            IsPaid = false;
            IsCanceled = false;
            OrderItems = new List<OrderItem>();
        }

        public void PaymentSuccess(int refId)
        {
            IsPaid = true;
            if (refId != 0)
                RefId = refId;
        }

        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void Add(OrderItem item)
        {
            OrderItems.Add(item);
        }
    }
}
