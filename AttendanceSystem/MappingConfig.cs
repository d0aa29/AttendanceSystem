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

            CreateMap<EmpEditProfileDTO, Employee>()
          .ForMember(dest => dest.DepartmentId, opt => opt.Condition(src => src.DepartmentId > 0))
          // .ForMember(dest => dest.DepartmentId, opt => opt.Condition(src => src.DepartmentId.HasValue))
          .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));



            CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : "No Department"))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : "No User"));

            // Map between Employee and EmployeeCreateDTO
            CreateMap<Employee, EmpShiftsUpdateDTO>().ReverseMap();


          // Map between LeaveRequest and LeaveRequestDTO
            CreateMap<LeaveRequest, LeaveRequestDTO>().ReverseMap();

            // Map between LeaveRequest and RequestCreateDTO
            CreateMap<LeaveRequest, RequestCreateDTO>().ReverseMap();

        }
    }
    
}
