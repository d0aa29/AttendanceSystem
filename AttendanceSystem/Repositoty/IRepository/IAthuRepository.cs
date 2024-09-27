using AttendanceSystem.Models;
using AttendanceSystem.Models.DTO;
using Repository.IRepository;
using System.Linq.Expressions;


namespace AttendanceSystem.Repositoty.IRepository {
    public interface IAthuRepository : IRepository<ApplicationUser>
    {

        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
      
    }

}

