using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class RequestUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public TimeSpan StartDate { get; set; }
        public TimeSpan EndDate { get; set; }
        public int EmployeeId { get; set; }
       // public string ApprovalStatus { get; set; } = "pending...";
    }
}
