using AttendanceSystem.Models;
using Microsoft.AspNetCore.Identity;
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

            // Seed data for departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "HR", Location = "Building A" },
                new Department { Id = 2, Name = "IT", Location = "Building B" },
                new Department { Id = 3, Name = "Sales", Location = "Building C" }
            );

            // Password hasher to hash passwords
            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed data for ApplicationUsers
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1", // User 1 (Admin)
                    UserName = "admin@system.com",
                    NormalizedUserName = "ADMIN@SYSTEM.COM",
                    Email = "admin@system.com",
                    NormalizedEmail = "ADMIN@SYSTEM.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "AdminPassword123!")
                },
                new ApplicationUser
                {
                    Id = "2", // User 2 (Employee)
                    UserName = "jane@system.com",
                    NormalizedUserName = "JANE@SYSTEM.COM",
                    Email = "jane@system.com",
                    NormalizedEmail = "JANE@SYSTEM.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "JanePassword123!")
                }
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
                    UserId = null // No user account
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
                    UserId = "2" // Linked to User 2 (jane@system.com)
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
                    UserId = null // No user account
                }
            );

            // Seed data for shifts
            modelBuilder.Entity<Shift>().HasData(
                new Shift { Id = 1, Num = 1, From = new TimeSpan(8, 0, 0), To = new TimeSpan(16, 0, 0) },
                new Shift { Id = 2, Num = 2, From = new TimeSpan(16, 0, 0), To = new TimeSpan(0, 0, 0) },
                new Shift { Id = 3, Num = 3, From = new TimeSpan(0, 0, 0), To = new TimeSpan(8, 0, 0) }
               
            );

            // Many-to-many relationship: Employee - Shift
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Shifts)
                .WithMany(s => s.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeShift",
                    j => j.HasOne<Shift>().WithMany().HasForeignKey("ShiftId"),
                    j => j.HasOne<Employee>().WithMany().HasForeignKey("EmployeeId")
                );

            // Seed data for EmployeeShift (assign shifts to employees)
            modelBuilder.Entity("EmployeeShift").HasData(
                new { EmployeeId = 2, ShiftId = 1 }, // Jane Smith assigned to Shift 1
                new { EmployeeId = 2, ShiftId = 2 }, // Jane Smith assigned to Shift 2
                new { EmployeeId = 3, ShiftId = 3 }  // Alice Johnson assigned to Shift 3
            );

            // Optionally: Seed roles if you're using them
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Employee", NormalizedName = "EMPLOYEE" }
            );

            // Assign roles to users (if applicable)
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // Admin role for admin user
                new IdentityUserRole<string> { UserId = "2", RoleId = "2" }  // Employee role for Jane
            );
        }

      

    }
}
