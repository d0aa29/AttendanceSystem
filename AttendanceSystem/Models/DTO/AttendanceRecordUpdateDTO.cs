using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class AttendanceRecordUpdateDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
      //  public DateTime? Date { get; set; }= DateTime.Now;
       // public TimeSpan CheckIn { get; set; }= DateTime.UtcNow.TimeOfDay;
      //  public TimeSpan CheckOut { get; set; } = DateTime.UtcNow.TimeOfDay;
       // public string OutStatus { get; set; }
     //   public string InStatus { get; set; }
        public string? Note { get; set; }

        //[Required]
        //public int EmployeeId { get; set; }
        //[ForeignKey("EmployeeId")]
        //public Employee Employee { get; set; }

        //[Required]
        //public int ShiftId { get; set; } 

        //[ForeignKey("ShiftId")]
        //public Shift Shift { get; set; }  
    }
}
