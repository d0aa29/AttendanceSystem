using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class ShiftUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Num { get; set; }
        [Required]
        public TimeSpan From { get; set; }
        [Required]
        public TimeSpan To { get; set; }
    }
}
