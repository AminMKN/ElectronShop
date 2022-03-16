namespace DiscountManagement.Application.Contracts.Discount
{
    public class DiscountViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DiscountRate { get; set; }
        public string Product { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public string CreationDate { get; set; }
    }
}
