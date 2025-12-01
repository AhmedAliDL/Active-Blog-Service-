using Active_Blog_Service.Models;
using Active_Blog_Service.Repositories.Contracts;
using Active_Blog_Service.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Active_Blog_Service.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICommentRepository _commentRepository;

        public CommentController(UserManager<User> userManager, ICommentRepository commentRepository)
        {
            _userManager = userManager;
            _commentRepository = commentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(BlogCommentViewModel blogCommentViewModel)
        {

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            blogCommentViewModel.comment.UserId = user.Id;
            _commentRepository.AddComment(blogCommentViewModel.comment);

            return RedirectToAction("Details", "Blog", new { id = blogCommentViewModel.comment.BlogId });

        }
        [HttpPost]
        public IActionResult EditComment(int commentId)
        {
            var comment = _commentRepository.GetComment(commentId);
            return View(comment);
        }
        [HttpPost]
        public IActionResult SaveCommentEdit(int commentId, string commentContent)
        {
            var comment = _commentRepository.GetComment(commentId);

            _commentRepository.EditComment(comment, commentContent);

            return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }
        [HttpPost]
        public IActionResult DeleteComment(int commentId)
        {
            var comment = _commentRepository.GetComment(commentId);
            int blogId = comment.BlogId;
            _commentRepository.DeleteComment(comment);

            return RedirectToAction("Details","Blog", new { id = blogId });
        }
    }
}
