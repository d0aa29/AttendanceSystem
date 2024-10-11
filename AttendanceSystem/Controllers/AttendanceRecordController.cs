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
        [HttpGet("AllAttendanceRecord")]
        [Authorize(Roles = " Admin, Manager")]
        public async Task<ActionResult<APIResponse>> GetAllEmployeeRecords()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId);
                //  var reqcordList = await _unitOfWork.AttendanceRecord.GetAll(u => u.EmployeeId == employee.Id, false, includProperties: "Employee");


                if (employee == null)
                {
                    return NotFound();
                }
                // IEnumerable<AttendanceRecord>
                // var reqcordList = await _unitOfWork.AttendanceRecord.GetAll(x => x.EmployeeId == employee.Id);
                IEnumerable<AttendanceRecord> reqcordList;

                // Check if the user is an admin or a manager
                if (User.IsInRole("Admin"))
                {
                    // Admin can access all attendance records
                    reqcordList = await _unitOfWork.AttendanceRecord.GetAll(includProperties: "Employee");
                }
                else if (User.IsInRole("Manager"))
                {
                    // Manager can only access attendance records for employees in their department
                    reqcordList = await _unitOfWork.AttendanceRecord.GetAll(x => x.Employee.DepartmentId == employee.DepartmentId, includProperties: "Employee");
                }
                else
                {

                    return Forbid();
                }
                var ReqcordListDTO = _mapper.Map<IEnumerable<AttendanceRecordDTO>>(reqcordList);
                _response.Result = ReqcordListDTO;

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("MyAttendanceRecord")]
        [Authorize(Roles = "Employee, Admin, Manager")]
        public async Task<ActionResult<APIResponse>> GetEmployeeRecords()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId);
                //  var reqcordList = await _unitOfWork.AttendanceRecord.GetAll(u => u.EmployeeId == employee.Id, false, includProperties: "Employee");


                if (employee == null)
                {
                    return NotFound();
                }
                // IEnumerable<AttendanceRecord>
                var reqcordList = await _unitOfWork.AttendanceRecord.GetAll(x => x.EmployeeId == employee.Id);

                var ReqcordListDTO = _mapper.Map<IEnumerable<AttendanceRecordDTO>>(reqcordList);
                _response.Result = ReqcordListDTO;

                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

      

    }
}
