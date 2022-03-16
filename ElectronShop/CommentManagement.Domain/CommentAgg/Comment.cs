using _01_Framework.Domain;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : EntityBase
    {
        public int AccountId { get; private set; }
        public string Message { get; private set; }
        public int OwnerRecordId { get; private set; }
        public int Type { get; private set; }
        public bool IsConfirmed { get; private set; }

        public Comment(int accountId, string message, int ownerRecordId, int type)
        {
            AccountId = accountId;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            IsConfirmed = false;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsConfirmed = false;
        }
    }
}
