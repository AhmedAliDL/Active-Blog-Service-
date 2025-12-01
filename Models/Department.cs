using System.ComponentModel.DataAnnotations;

namespace Active_Blog_Service.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(30)]
        [MinLength(3)]
        public string ManagerName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
