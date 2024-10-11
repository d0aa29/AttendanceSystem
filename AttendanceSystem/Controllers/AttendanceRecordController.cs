using AttendanceSystem.Models;
using AttendanceSystem.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Net;
using System.Security.Claims;

namespace AttendanceSystem.Controllers
{
    [Route("api/AttendanceRecord")]
    [ApiController]
    public class AttendanceRecordController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly APIResponse _response;
        private readonly IMapper _mapper;
        public AttendanceRecordController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _response = new();
            _mapper = mapper;
        }
     
    }
}
