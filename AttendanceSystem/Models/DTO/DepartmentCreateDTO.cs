using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class DepartmentCreateDTO
    {
       
        [Required]
        public string Name { get; set; }
        public string? Location { get; set; }
    }
}
