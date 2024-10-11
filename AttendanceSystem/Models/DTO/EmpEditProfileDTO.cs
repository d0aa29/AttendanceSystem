namespace AttendanceSystem.Models.DTO
{
    public class EmpEditProfileDTO
    {
        public int SSN { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinedOn { get; set; }
        public int? DepartmentId { get; set; }
    }
}
