namespace ShopManagement.Application.Contracts.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int RefId { get; set; }
        public int AccountId { get; set; }
        public string FullName { get; set; }
        public string IssueTrackingNo { get; set; }
        public string CreationDate { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCanceled { get; set; }
    }
}