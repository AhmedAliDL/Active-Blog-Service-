using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.Models
{
    public class User : IdentityUser
    {
        [MaxLength(30)]
        [MinLength(3)]
        public string FName { get; set;}
        [MaxLength(30)]
        [MinLength(3)]
        public string LName { get; set;}
        public string? Image {  get; set;}
        public string Address { get; set; }

        public virtual ICollection<Blog> Blogs { get; set;}
        public virtual ICollection<Comment> Comments { get; set;}
    }
}
