using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class EmployeeCreateDTO
    {
        public int Id { get; set; }
        public int SSN { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinedOn { get; set; }
        public int? DepartmentId { get; set; }
        public string? UserId { get; set; }
    }
}
