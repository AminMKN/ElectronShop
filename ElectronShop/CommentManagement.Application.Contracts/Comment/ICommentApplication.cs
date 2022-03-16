using _01_Framework.Application;

namespace CommentManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        OperationResult Confirm(int id);
        OperationResult Cancel(int id);
        Task<List<CommentViewModel>> Search(CommentSearchModel searchModel);
    }
}