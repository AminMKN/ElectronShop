using _01_Framework.Domain;
using CommentManagement.Application.Contracts.Comment;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<int, Comment>
    {
        Task<List<CommentViewModel>> Search(CommentSearchModel searchModel);
    }
}