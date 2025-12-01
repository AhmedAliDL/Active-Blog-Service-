using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [MinLength(3)]
        public string CommentContent { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public int BlogId { get; set; }
        public string UserId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual User User { get; set; }

    }
}
