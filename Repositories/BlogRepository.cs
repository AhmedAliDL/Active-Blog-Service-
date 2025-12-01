using Active_Blog_Service.Context;
using Active_Blog_Service.Models;
using Active_Blog_Service.Repositories.Contracts;
using Active_Blog_Service.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Active_Blog_Service.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }
        public DbSet<Blog> GetBlogs()
        {
            return _context.Blogs;
        }
        public async Task AddBlogAsync(string userId,BlogForEditingOrAddingViewModel addBlogViewModel)
        {
            string imagePath = "";
            if (addBlogViewModel.ImageFile != null && addBlogViewModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/blogImages");
                var fileName = Path.GetFileName(addBlogViewModel.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await addBlogViewModel.ImageFile.CopyToAsync(stream);
                }

                imagePath = $"/blogImages/{fileName}";

                Blog blog = new ()
                {
                    Title = addBlogViewModel.Title,
                    Category = addBlogViewModel.Category,
                    BlogContent = addBlogViewModel.BlogContent,
                    Image = imagePath,
                    UserId = userId,
                };

                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();
            }

        }
        public Blog GetBlogById(int id)
        {
            return _context.Blogs.FirstOrDefault(b => b.Id == id)!;
        }
        public List<Blog> GetBlogsByDate(DateOnly date)
        {
            return _context.Blogs.Where(b => b.CreatedDate >= date).ToList();
        }

        public List<Blog> GetFromLast(int count)
        {
            return GetBlogs().OrderByDescending(b=>b.Id).Take(count).ToList();
        }
        public void DeleteBlog(int blogId)
        {
            var blog = GetBlogById(blogId);
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }
        public async Task EditBlogAsync(int id, BlogForEditingOrAddingViewModel blogViewModel)
        {
            var blog = GetBlogById(id);
            Console.WriteLine(blog.Title);
            if (blog == null) return; // Handle the case where blog might not exist

            blog.BlogContent = blogViewModel.BlogContent;
            blog.Title = blogViewModel.Title;
            blog.Category = blogViewModel.Category;

            if (blogViewModel.ImageFile != null && blogViewModel.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var fileName = Path.GetFileName(blogViewModel.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await blogViewModel.ImageFile.CopyToAsync(stream);
                }

                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.Image.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                blog.Image = $"/images/{fileName}";
            }

            await _context.SaveChangesAsync();
        }

       

    }
}
