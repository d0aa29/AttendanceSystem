using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class RequestCreateDTO
    {
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }
        [Required]
        public TimeSpan StartDate { get; set; }
        [Required]
        public TimeSpan EndDate { get; set; }
        public string Reason { get; set; }

        //[Required]
        //public int EmployeeId { get; set; }
    }
}
