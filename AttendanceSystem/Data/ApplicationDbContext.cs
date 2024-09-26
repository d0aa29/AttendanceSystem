using AttendanceSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for departments (you need to seed this before seeding employees with DepartmentId)
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR", Location = "Building A" },
                new Department { Id = 2, Name = "IT", Location = "Building B" },
                new Department { Id = 3, Name = "Sales", Location = "Building C" }
            );

            // Seed data for employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    SSN = 123456789,
                    Name = "John Doe",
                    Gender = "Male",
                    BirthDate = new DateTime(1990, 1, 1),
                    JoinedOn = new DateTime(2022, 3, 15),
                    DepartmentId = 1,
                    UserId = null // This can be null if the employee doesn't have a User account
                },
                new Employee
                {
                    Id = 2,
                    SSN = 987654321,
                    Name = "Jane Smith",
                    Gender = "Female",
                    BirthDate = new DateTime(1992, 5, 21),
                    JoinedOn = new DateTime(2021, 7, 1),
                    DepartmentId = 2,
                    UserId = null
                },
                new Employee
                {
                    Id = 3,
                    SSN = 112233445,
                    Name = "Alice Johnson",
                    Gender = "Female",
                    BirthDate = new DateTime(1985, 9, 10),
                    JoinedOn = new DateTime(2020, 2, 10),
                    DepartmentId = 3,
                    UserId = null
                }
            );
        }
    }
}
