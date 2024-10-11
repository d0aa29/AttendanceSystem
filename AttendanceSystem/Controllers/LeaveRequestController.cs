using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Models.DTO;
using AttendanceSystem.Repositoty.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Net;
using System.Security.Claims;

namespace AttendanceSystem.Controllers
{
    [Route("api/Request")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly APIResponse _response;
        private readonly IMapper _mapper;
        public LeaveRequestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _response = new();
            _mapper = mapper;
        }

       
    }
}
