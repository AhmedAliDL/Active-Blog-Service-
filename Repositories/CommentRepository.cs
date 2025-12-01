using Active_Blog_Service.Context;
using Active_Blog_Service.Models;
using Active_Blog_Service.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace Active_Blog_Service.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Comment> GetComments()
        {
            return _context.Comments.ToList();
        }
        public List<Comment> GetCommentsOfBlogOrderByDateTime(int blogId)
        {
            return _context.Comments.Where(c=>c.BlogId == blogId).OrderBy(c=>c.CreatedDateTime).ToList();
        }
        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public Comment GetComment(int commentId)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == commentId)!;
        }
        public void EditComment(Comment oldComment,string commentContent)
        {
            oldComment.CommentContent = commentContent;
            _context.SaveChanges();
        }
        public void DeleteComment(Comment comment)
        {
            
            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
         
       
    }
}
