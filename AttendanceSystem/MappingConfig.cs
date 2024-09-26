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

         
        }
    }
    
}
