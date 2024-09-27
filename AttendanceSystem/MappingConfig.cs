using AttendanceSystem.Models;
using AutoMapper;
using AttendanceSystem.Models.DTO;

namespace AttendanceSystem
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Map between Department and DepartmentCreateDTO
            CreateMap<Department, DepartmentCreateDTO>().ReverseMap();

            // Map between Department and DepartmentUpdateDTO
            CreateMap<Department, DepartmentUpdateDTO>().ReverseMap();

            // Map between shift and shiftDTO
            CreateMap<Shift, ShiftDTO>().ReverseMap();

            // Map between shift and ShiftUpdateDTO
            CreateMap<Shift, ShiftUpdateDTO>().ReverseMap();


            // Map between ApplicationUser and UserDTO
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

            // Map between ApplicationUser and UserUpdateDTO
            CreateMap<ApplicationUser, UserUpdateDTO>().ReverseMap();
            // Map between Employee and EmployeeCreateDTO
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();

            // Map between Employee and EmployeeCreateDTO
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();

           

        }
    }
    
}
