using _01_Framework.Application;
using _01_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : RepositoryBase<int, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;
        private readonly AccountContext _accountContext;

        public CommentRepository(CommentContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public async Task<List<CommentViewModel>> Search(CommentSearchModel searchModel)
        {
            var comments = await _context.Comments.Select(x => new CommentViewModel()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                Message = x.Message,
                IsConfirmed = x.IsConfirmed,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

            foreach (var comment in comments)
            {
                var account = await _accountContext.Accounts.FirstOrDefaultAsync(x => x.Id == comment.AccountId);
                comment.Name = account.FullName;
            }

            return comments.Where(x => x.IsConfirmed == searchModel.IsConfirmed).ToList();
        }
    }
}