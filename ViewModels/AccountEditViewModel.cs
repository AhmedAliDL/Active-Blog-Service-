using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.ViewModels
{
    public class AccountEditViewModel 
    {
        [MaxLength(30)]
        [MinLength(3)]
        public string? FName { get; set; }
        [MaxLength(30)]
        [MinLength(3)]
        public string? LName { get; set; }
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[cC][oO][mM]$",ErrorMessage ="Email must end with .com")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public IFormFile? ImageFile { get; set; }
        [MaxLength(11)]
        [MinLength(11)]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirmed Password field must match Password field")]
        public string? ConfirmPassword {  get; set; }

    }

}
