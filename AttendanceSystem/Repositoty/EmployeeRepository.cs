using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Repositoty.IRepository;
using Repository;

namespace AttendanceSystem.Repositoty
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task Update(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
        }
    }
}
