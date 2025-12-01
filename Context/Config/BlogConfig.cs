using Active_Blog_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Active_Blog_Service.Context.Config
{
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
           builder.HasKey(b => b.Id);
           builder.Property(b=>b.Id).ValueGeneratedNever();

           builder.HasOne(b => b.User)
                 .WithMany().HasForeignKey(b => b.UserId);

        }
    }
}
