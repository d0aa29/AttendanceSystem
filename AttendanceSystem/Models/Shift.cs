using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models
{
    public class Shift
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int Num { get; set; }
        public  DateTime From { get; set; }
        public DateTime To { get; set; }

        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
