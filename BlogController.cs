using Active_Blog_Service.Models;
using Active_Blog_Service.Repositories.Contracts;
using Active_Blog_Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Active_Blog_Service.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IBlogRepository _blogRepository;
        private readonly ICommentRepository _commentRepository;

        public BlogController(UserManager<User> userManager, IBlogRepository blogRepository, ICommentRepository commentRepository)
        {
            _userManager = userManager;
            _blogRepository = blogRepository;
            _commentRepository = commentRepository;
        }
        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 3)
        {

            var blogs = _blogRepository.GetBlogs().Select(b => new ShowBlogViewModel
            {
                Id = b.Id,
                Title = b.Title,
                CreatedDate = b.CreatedDate,
                Image = b.Image,
                UserId = b.UserId,
            }).ToList();

            var blogViewModel = new PaginationBlogViewModel();
            var totalBlogs = blogs.Count;

            blogViewModel.TotalPages = (int)Math.Ceiling(totalBlogs / (double)pageSize);
            blogViewModel.PageSize = pageSize;
            blogViewModel.Currentpage = page;
            blogViewModel.ShowBlogList = blogs;
            return View(blogViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(BlogForEditingOrAddingViewModel addBlogViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);

                await _blogRepository.AddBlogAsync(user.Id, addBlogViewModel);

                return RedirectToAction("Index", "Home");
            }
            return View();  
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var blog = _blogRepository.GetBlogById(id);

            if (blog == null)
            {
                return NotFound();  // or return a custom error view
            }

            var comments = _commentRepository.GetCommentsOfBlogOrderByDateTime(id);

            if (comments != null && comments.Count > 0)
            {
                blog.Comments = comments;
            }

            BlogCommentViewModel blogCommentViewModel = new()
            {
                blog = blog
            };
            return View(blogCommentViewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteBlog()
        {

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var blogs = _blogRepository.GetBlogs().Where(b => b.UserId == user.Id).ToList();
            return View(blogs);
        }

        [HttpPost]
        public IActionResult DeleteBlog(int blogId)
        {
            _blogRepository.DeleteBlog(blogId);

            // Redirect to the list of blogs after deletion
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> ShowBlogs()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var blogs = _blogRepository.GetBlogs().Where(b=>b.UserId == user.Id).ToList();
            return View(blogs);
        }
        [HttpGet]
        public IActionResult EditBlog(int blogId)
        {
            var blog = _blogRepository.GetBlogById(blogId);

            var viewModel = new BlogForEditingOrAddingViewModel();

            viewModel.Id = blogId;
            viewModel.Title = blog.Title;
            viewModel.BlogContent = blog.BlogContent;
            viewModel.Category = blog.Category;
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEdit(int blogId, BlogForEditingOrAddingViewModel blogViewModel)
        {
            
            await _blogRepository.EditBlogAsync(blogId, blogViewModel);

            return RedirectToAction("Index");
        }
       

    }
}
