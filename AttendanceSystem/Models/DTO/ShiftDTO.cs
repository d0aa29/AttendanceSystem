using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class ShiftDTO
    {
        [Required]
        public int Num { get; set; }
        [Required]
        public TimeSpan From { get; set; }
        [Required]
        public TimeSpan To { get; set; }
    }
}
