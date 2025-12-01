using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.ViewModels
{
    public class SendMailViewModel
    {
        [MaxLength(40)]
        [MinLength(3)]
        public string Subject { get; set; }
        [MinLength(3)]
        public string Body { get; set; }
    }
}
