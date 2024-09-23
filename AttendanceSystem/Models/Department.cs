using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class Department
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Location { get; set; }
       // public string Manager { get; set; }
       public List<Employee> Employees { get; set; }
    }
}
