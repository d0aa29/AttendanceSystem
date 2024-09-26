using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Repositoty.IRepository;
using Azure.Core;
using Repository;

namespace AttendanceSystem.Repositoty
{
    public class LeaveRequestRepository : Repository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Update(LeaveRequest request)
        //{
        //    _db.LeaveRequests.Update(request); 
        //}
        public async Task Update(LeaveRequest request)
        {
            _db.LeaveRequests.Update(request);
            await _db.SaveChangesAsync();
        }
    }
}
