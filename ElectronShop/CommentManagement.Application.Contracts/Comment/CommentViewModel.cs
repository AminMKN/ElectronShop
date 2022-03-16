namespace CommentManagement.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}