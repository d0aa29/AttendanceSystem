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


        [HttpPost("CheckIn")]
        [Authorize(Roles = "Employee, Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAttendanceRecord([FromBody] AttendanceRecordCreateDTO AttendanceDTO)
        {
            try
            {

                if (AttendanceDTO == null)
                    return BadRequest();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID

                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, includProperties: "Department,User,Shifts");

                if (employee == null)
                {
                    return NotFound();
                }

                AttendanceRecord attendance = _mapper.Map<AttendanceRecord>(AttendanceDTO);

                attendance.EmployeeId = employee.Id;
                attendance.Date = DateTime.Now;
                attendance.CheckIn = DateTime.Now.TimeOfDay;

                Shift shift = await _unitOfWork.Shift.Get(x => x.Id == AttendanceDTO.ShiftId);


                var shiftStart = shift.From;
                var shiftEnd = shift.To;
                TimeSpan gracePeriod = new TimeSpan(0, 15, 0); // 15-minute grace period

                // Check if the shift spans over midnight
                bool isShiftOverMidnight = shiftStart > shiftEnd;

                if (isShiftOverMidnight)
                {
                    // Shift crosses midnight (e.g., 10 PM to 6 AM)
                    if (attendance.CheckIn >= shiftStart || attendance.CheckIn <= shiftEnd)
                    {
                        // Check if employee is within the grace period after shift start
                        if (attendance.CheckIn > shiftStart.Add(gracePeriod))
                        {
                            attendance.InStatus = "Late";
                        }
                        else
                        {
                            attendance.InStatus = "On Time"; // Checked in within the grace period
                        }
                    }
                    else
                    {
                        attendance.InStatus = "Late"; // Checked in outside the shift window
                    }
                }
                else
                {
                    // Regular shift that doesn't cross midnight
                    if (attendance.CheckIn > shiftStart.Add(gracePeriod))
                    {
                        attendance.InStatus = "Late"; // Late beyond grace period
                    }
                    else
                    {
                        attendance.InStatus = "On Time"; // Checked in within the grace period
                    }
                }



                await _unitOfWork.AttendanceRecord.Create(attendance);
                // await _dbvilla.Save();
                _response.Result = _mapper.Map<AttendanceRecordCreateDTO>(attendance);
                _response.StatusCode = HttpStatusCode.OK;
                // return Ok(_response);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[HttpPut("update/{id:int}", Name = "CheckOut")]
        [HttpPut("CheckOut")]
        [Authorize(Roles = "Employee, Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CheckOut(int id, [FromBody] AttendanceRecordUpdateDTO AttendanceDTO)
        {
            try
            {

                if (AttendanceDTO == null)
                    return BadRequest();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID

                //  var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, includProperties: "Department,User,Shifts");
                if (id != AttendanceDTO.Id || AttendanceDTO == null)
                    return BadRequest();
                var req = await _unitOfWork.AttendanceRecord.Get(u => u.Id == id, false, includProperties: "Employee");
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, false, includProperties: "Department,User,Shifts");

                if (req == null || req.EmployeeId != employee.Id)
                {
                    return BadRequest("You can only update your own leave requests.");
                }
                if (employee == null)
                {
                    return NotFound();
                }

                AttendanceRecord attendance = _mapper.Map<AttendanceRecord>(AttendanceDTO);

                attendance.CheckOut = DateTime.Now.TimeOfDay;
                attendance.ShiftId = req.ShiftId;
                attendance.CheckIn = req.CheckIn;
                attendance.Date = req.Date;
                attendance.InStatus = req.InStatus;
                attendance.EmployeeId = req.EmployeeId;
                if (AttendanceDTO.Note == null)
                    attendance.Note = req.Note;
                Shift shift = await _unitOfWork.Shift.Get(x => x.Id == attendance.ShiftId);
                var shiftEnd = shift.To;
                var shiftStart = shift.From;

                // Handle the case where the shift spans midnight
                bool isShiftOverMidnight = shiftStart > shiftEnd;
                if (isShiftOverMidnight)
                {
                    // Case where the shift ends after midnight
                    if (attendance.CheckOut > shiftEnd && attendance.CheckOut < shiftStart)
                    {
                        attendance.OutStatus = "Early";
                    }
                    else
                    {
                        attendance.OutStatus = "Overtime";
                    }
                }
                else
                {
                    // Regular case where the shift doesn't cross midnight
                    if (attendance.CheckOut < shiftEnd)
                    {
                        attendance.OutStatus = "Early";
                    }
                    else
                    {
                        attendance.OutStatus = "Overtime";
                    }
                }
                await _unitOfWork.AttendanceRecord.Update(attendance);

                _response.Result = _mapper.Map<AttendanceRecordDTO>(attendance);
                _response.StatusCode = HttpStatusCode.OK;
                // return Ok(_response);

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
