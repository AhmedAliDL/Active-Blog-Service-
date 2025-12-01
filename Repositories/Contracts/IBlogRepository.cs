using Active_Blog_Service.Models;
using Active_Blog_Service.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Active_Blog_Service.Repositories.Contracts
{
    public interface IBlogRepository : IAddScopedService
    {
        DbSet<Blog> GetBlogs();
        Task AddBlogAsync(string userId, BlogForEditingOrAddingViewModel addBlogViewModel);
        Blog GetBlogById(int id);
        List<Blog> GetBlogsByDate(DateOnly date);
        List<Blog> GetFromLast(int count);
        void DeleteBlog(int blogId);
        Task EditBlogAsync(int id, BlogForEditingOrAddingViewModel blogViewModel);
    }
}
