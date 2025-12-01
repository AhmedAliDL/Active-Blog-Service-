using Active_Blog_Service.Models;
using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.ViewModels
{
    public class BlogForEditingOrAddingViewModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string Category { get; set; }
        public IFormFile ImageFile { get; set; }
        [MinLength(50)]
        public string BlogContent { get; set; }
       
    }
}
