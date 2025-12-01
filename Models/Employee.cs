using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [MinLength(3)]
        public string  Name { get; set; }
        [MaxLength(40)]
        [MinLength(3)]
        public string Role { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(500)]
        [MinLength(10)]
        public string RoleDescription { get; set; }
        public string Image { get; set; }
        public string? Address { get; set; }
        [MaxLength(11)]
        [MinLength(11)]
        public string PhoneNumber { get; set; }
        [Range(20,60)]
        public int? Age { get; set; }
        [Range(2000,10000)]
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

    }
}
