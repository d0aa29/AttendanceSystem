using AttendanceSystem.Models;
using AttendanceSystem.Models.DTO;
using Repository.IRepository;
using System.Linq.Expressions;


namespace AttendanceSystem.Repositoty.IRepository {
    public interface IUserRepository :IRepository<ApplicationUser>
    {
        Task Update(ApplicationUser user);
       
    }

}

