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
    public class UserRepository :  Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
          
            _db = db;
        }
       
        public async Task Update(ApplicationUser user)
        {
            _db.ApplicationUsers.Update(user);
            await _db.SaveChangesAsync();
        }

       
    }
}
