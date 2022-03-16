namespace ShopManagement.Application.Contracts.Order
{
    public class OrderSearchModel
    {
        public int AccountId { get; set; }
        public string IssueTrackingNo { get; set; }
        public bool IsCanceled { get; set; }
    }
}
