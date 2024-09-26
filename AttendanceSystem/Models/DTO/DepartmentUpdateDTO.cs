using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class DepartmentUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Location { get; set; }
    }
}
