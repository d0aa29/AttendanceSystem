using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class AttendanceRecordDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public TimeSpan CheckIn { get; set; } = DateTime.UtcNow.TimeOfDay;
        public TimeSpan CheckOut { get; set; } = DateTime.UtcNow.TimeOfDay;
        public string InStatus { get; set; } = "In Time";
        public string OutStatus { get; set; } = "Not Checked Out";
        public string? Note { get; set; }

        public int ShiftId { get; set; } 

    
    }
}
