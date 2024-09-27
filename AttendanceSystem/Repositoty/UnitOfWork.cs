using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Repository;
//using AttendanceSystem.Repository.IRepository;
using AttendanceSystem.Repositoty;
using AttendanceSystem.Repositoty.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repository.IRepository;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public IAttendanceRecordRepository AttendanceRecord { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public ILeaveRequestRepository LeaveRequest { get; private set; }
        public IShiftRepository Shift { get; private set; }
        public IUserRepository User { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AttendanceRecord=new AttendanceRecordRepository(db);
            Department=new DepartmentRepository(db);
            Employee=new EmployeeRepository(db);
            LeaveRequest=new LeaveRequestRepository(db);
            Shift = new ShiftRepository(db);
            User=new UserRepository(db);
          //  
        }
      
        //public async Task Save()
        //{
        //    await _db.SaveChangesAsync();
        //}



    }
    }
