using AttendanceSystem.Models;
using Repository.IRepository;

namespace AttendanceSystem.Repositoty.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task Update(Employee employee);
       

    }
    
}
