using AttendanceSystem.Models;
using Repository.IRepository;

namespace AttendanceSystem.Repositoty.IRepository
{
    public interface IShiftRepository : IRepository<Shift>
    {
        void Update(Shift shift);
       

    }
    
}
