using Active_Blog_Service.Models;

namespace Active_Blog_Service.Repositories.Contracts
{
    public interface ICommentRepository : IAddScopedService
    {
        List<Comment> GetCommentsOfBlogOrderByDateTime(int blogId);
        void AddComment(Comment comment);
        Comment GetComment(int commentId);
        void EditComment(Comment oldComment, string commentContent);
        void DeleteComment(Comment comment);
        List<Comment> GetComments();


    }
}
