using _01_Framework.Application.AuthHelper;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Admin.Pages.Comments
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public CommentSearchModel SearchModel;
        public List<CommentViewModel> Comments;
        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public async Task<IActionResult> OnGet(CommentSearchModel searchModel)
        {
            if (ClaimChecker.CheckCommentManagementClaim())
            {
                Comments = await _commentApplication.Search(searchModel);
                return Page();
            }

            return RedirectToPage("/Account/AccessDenied");
        }

        public IActionResult OnGetConfirm(int id)
        {
            _commentApplication.Confirm(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(int id)
        {
            _commentApplication.Cancel(id);
            return RedirectToPage("./Index");
        }
    }
}
