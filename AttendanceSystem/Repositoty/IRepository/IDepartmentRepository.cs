using AttendanceSystem.Models;
using Repository.IRepository;

namespace AttendanceSystem.Repositoty.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task Update(Department department);

    }
    
}
