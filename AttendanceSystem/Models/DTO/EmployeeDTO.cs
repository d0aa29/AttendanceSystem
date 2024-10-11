using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public int SSN { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinedOn { get; set; }
        public string? DepartmentName { get; set; }
        public string? UserName { get; set; }
        public List<ShiftDTO>? Shifts { get; set; } = new List<ShiftDTO>();
    }
}
