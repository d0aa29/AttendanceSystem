using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Repositoty;
using AttendanceSystem.Repositoty.IRepository;
using Repository.IRepository;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IAttendanceRecordRepository AttendanceRecord { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public ILeaveRequestRepository LeaveRequest { get; private set; }
        public IShiftRepository Shift { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            AttendanceRecord=new AttendanceRecordRepository(db);
            Department=new DepartmentRepository(db);
            Employee=new EmployeeRepository(db);
            LeaveRequest=new LeaveRequestRepository(db);
            Shift = new ShiftRepository(db);
        }

        //public async Task Save()
        //{
        //    await _db.SaveChangesAsync();
        //}



    }
}
