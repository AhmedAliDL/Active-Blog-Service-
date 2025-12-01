using Active_Blog_Service.Models;

namespace Active_Blog_Service.ViewModels
{
    public class BlogCommentViewModel
    {
        public Blog blog { get; set; }

        public Comment comment { get; set; } = new Comment();
    }
}
