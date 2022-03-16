using _01_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation = new OperationResult();
            var comment = new Comment(command.AccountId, command.Message, command.OwnerRecordId, command.Type);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Confirm(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Confirm();
            _commentRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Cancel(int id)
        {
            var operation = new OperationResult();
            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Cancel();
            _commentRepository.SaveChanges();
            return operation.Success();
        }

        public async Task<List<CommentViewModel>> Search(CommentSearchModel searchModel)
        {
            return await _commentRepository.Search(searchModel);
        }
    }
}
