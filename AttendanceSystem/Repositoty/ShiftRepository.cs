using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Repositoty.IRepository;
using Repository;

namespace AttendanceSystem.Repositoty
{
    public class ShiftRepository : Repository<Shift>, IShiftRepository
    {
        private readonly ApplicationDbContext _db;
        public ShiftRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Update(Shift shift)
        //{
        //    _db.Shifts.Update(shift);
        //}
        public async Task Update(Shift shift)
        {
            _db.Shifts.Update(shift);
            await _db.SaveChangesAsync();
        }
    }
}
