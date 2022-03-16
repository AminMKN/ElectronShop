namespace CommentManagement.Application.Contracts.Comment
{
    public class AddComment
    {
        public int AccountId { get; set; }
        public string Message { get; set; }
        public int OwnerRecordId { get; set; }
        public int Type { get; set; }
    }
}
