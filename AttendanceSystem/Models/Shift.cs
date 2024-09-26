using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class Shift
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int Num { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }

        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
