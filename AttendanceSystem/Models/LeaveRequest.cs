using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class LeaveRequest
    { 

        [Key]
        [Required]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }
        public TimeSpan? StartDate { get; set; }
        public TimeSpan? EndDate { get; set; }
        public string Reason { get; set; }
        public string ApprovalStatus { get; set; } = "pending...";

        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
