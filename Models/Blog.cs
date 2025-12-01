using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string Category { get; set; }
        public string Image { get; set; } 
        [MinLength(50)]
        public string BlogContent { get; set; }
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string UserId {  get; set; }

        public ICollection<Comment>? Comments { get; set; } = new List<Comment>();

        public virtual User User { get; set; }

    }
}
