using Active_Blog_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Active_Blog_Service.Context.Config
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c=>c.Id).ValueGeneratedNever();

            builder.HasOne(c => c.User)
                .WithMany().HasForeignKey(c => c.UserId);
            
            builder.HasOne(c => c.Blog)
                .WithMany().HasForeignKey(c => c.BlogId);

        }
    }
}
