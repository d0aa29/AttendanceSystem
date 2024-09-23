using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Repositoty.IRepository;
using Repository;

namespace AttendanceSystem.Repositoty
{
    public class AttendanceRecordRepository : Repository<AttendanceRecord>, IAttendanceRecordRepository
    {
        private readonly ApplicationDbContext _db;
        public AttendanceRecordRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AttendanceRecord record)
        {
            _db.AttendanceRecords.Update(record);
        }
    }
}
