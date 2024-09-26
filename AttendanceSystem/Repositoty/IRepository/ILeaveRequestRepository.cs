using AttendanceSystem.Models;
using Repository.IRepository;

namespace AttendanceSystem.Repositoty.IRepository
{
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        Task Update(LeaveRequest request);
       

    }
    
}
