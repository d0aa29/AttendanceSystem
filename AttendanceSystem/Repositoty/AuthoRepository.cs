using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Models.DTO;
//using AttendanceSystem.Repository.IRepository;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Repository;
using AttendanceSystem.Repositoty.IRepository;

namespace AttendanceSystem.Repository
{
    public class AuthoRepository :  Repository<ApplicationUser>, IAthuRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private string secretKey;
        public AuthoRepository(ApplicationDbContext db, IConfiguration configuration, 
            UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager) : base(db)
        {

            _db = db;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            secretKey = configuration.GetValue<string>("JWT:Secret");
        }

        public bool IsUniqueUser(string username)
        {
           if( _db.ApplicationUsers.FirstOrDefault(u => u.UserName == username)==null)
                return true;
           return false;
        }
       
      

        public async Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            ApplicationUser user = new ()
            {
               UserName= registerationRequestDTO.UserName,
             //  FullName = registerationRequestDTO.Name,
               Email=registerationRequestDTO.Email,
               NormalizedEmail=registerationRequestDTO.Email.ToUpper(),
             
            };
            try { 
                var result= await _userManager.CreateAsync(user, registerationRequestDTO.Password);
                if (result.Succeeded) {
                    //await _userManager.AddToRoleAsync(user, "admin");
                    //    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    //    {
                    //        await _roleManager.CreateAsync(new IdentityRole("admin"));
                    //        await _roleManager.CreateAsync(new IdentityRole("employee"));
                    //        await _roleManager.CreateAsync(new IdentityRole("manager"));
                    //    }

                    if (!await _roleManager.RoleExistsAsync(registerationRequestDTO.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(registerationRequestDTO.Role));
                    }

                    // Assign the user to the role
                    await _userManager.AddToRoleAsync(user, registerationRequestDTO.Role);

                    var userToReturn = _db.ApplicationUsers
                     .FirstOrDefault(u => u.UserName == registerationRequestDTO.UserName);
                    return _mapper.Map<UserDTO>(userToReturn);

                }
            }
            catch(Exception ex) {
            }
            
            return new UserDTO();
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(
              u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }
            //JWT
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDTO>(user),
                //  Role=roles.FirstOrDefault()

            };
            return loginResponseDTO;
        }

    }
}
