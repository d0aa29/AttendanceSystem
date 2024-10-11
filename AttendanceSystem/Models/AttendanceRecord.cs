using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class AttendanceRecord
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateTime? Date { get; set; } 
        public TimeSpan CheckIn { get; set; } 
        public TimeSpan? CheckOut { get; set; }
        public string InStatus { get; set; } 
        public string? OutStatus { get; set; } 
        public string? Note { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Required]
        public int ShiftId { get; set; } 

        [ForeignKey("ShiftId")]
        public Shift Shift { get; set; }  
    }
}
