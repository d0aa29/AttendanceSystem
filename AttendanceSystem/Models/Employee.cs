using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SSN { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string? ImgUrl { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinedOn { get; set; }

        // Foreign key to Department
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        // Foreign key to ApplicationUser
       // [Required]
        public string? UserId { get; set; }  
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public List<AttendanceRecord>? Attendances { get; set; } = new List<AttendanceRecord>();
        public List<LeaveRequest>? Requests { get; set; } = new List<LeaveRequest>();
        public ICollection<Shift>? Shifts { get; set; } = new List<Shift>();
    }
}
