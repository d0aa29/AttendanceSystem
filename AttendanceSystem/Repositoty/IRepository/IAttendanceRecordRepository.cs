using AttendanceSystem.Models;
using Repository.IRepository;

namespace AttendanceSystem.Repositoty.IRepository
{
    public interface IAttendanceRecordRepository : IRepository<AttendanceRecord>
    {
        void Update(AttendanceRecord attendance);
       

    }
    
}
