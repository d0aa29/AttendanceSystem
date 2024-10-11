using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class LeaveRequestDTO
    {
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }
        [Required]
        public TimeSpan StartDate { get; set; }
        [Required]
        public TimeSpan EndDate { get; set; }
        public string ApprovalStatus { get; set; } = "pending...";

    }
}
