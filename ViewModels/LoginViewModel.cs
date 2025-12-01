using System.ComponentModel.DataAnnotations;
using Active_Blog_Service.Models;

namespace Active_Blog_Service.ViewModels
{
    public class LoginViewModel 
    {
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[cC][oO][mM]$", ErrorMessage = "Email must end with .com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
