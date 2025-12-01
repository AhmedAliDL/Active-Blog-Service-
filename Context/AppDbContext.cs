using Active_Blog_Service.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Active_Blog_Service.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext():base()
        {
            
        }

        public AppDbContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = WebApplication.CreateBuilder().Configuration;

            var constr = config.GetSection("constr").Value;

            try
            {
                optionsBuilder.UseSqlServer(constr);
            }

            catch
            {
                throw new Exception();
            }

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().Property(e => e.Salary).HasColumnType("decimal(18,2)");
        }
    }
}
