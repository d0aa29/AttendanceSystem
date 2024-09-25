

using AttendanceSystem.Repositoty.IRepository;

namespace Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IAttendanceRecordRepository AttendanceRecord { get; }

        public IDepartmentRepository Department { get; }
        public IEmployeeRepository Employee { get; }

        public ILeaveRequestRepository LeaveRequest { get; }
        public IShiftRepository Shift { get; }

       // Task Save();
    }
}
