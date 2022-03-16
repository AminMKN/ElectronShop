namespace _02_ElectronShopQuery.Contracts.Comment
{
    public class CommentQueryModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; }
    }
}
