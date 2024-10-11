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
        [HttpGet("MyRequstes")]
        [Authorize(Roles = "Employee,Manager")]
        public async Task<ActionResult<APIResponse>> GetMyRequstes()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, includProperties: "Department,User,Shifts");

                if (employee == null)
                {
                    return NotFound();
                }
                // IEnumerable<LeaveRequest> 
                var RequestList = await _unitOfWork.LeaveRequest.GetAll(x => x.EmployeeId == employee.Id);
                var RequestListDTO = _mapper.Map<IEnumerable<LeaveRequestDTO>>(RequestList);
                _response.Result = RequestListDTO;
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


        [HttpPost]
        [Authorize(Roles = "Employee, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateRequest([FromBody] RequestCreateDTO RequestDTO)
        {
            try
            {

                if (RequestDTO == null)
                    return BadRequest();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID

                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, includProperties: "Department,User,Shifts");

                if (employee == null)
                {
                    return NotFound();
                }
                // RequestDTO.EmployeeId = employee.Id;

                //if (await  _unitOfWork.LeaveRequest.Get(x => x.Num == RequestDTO.Num) != null)
                //{
                //    ModelState.AddModelError("", "Request alredy exist");
                //    return BadRequest(ModelState);
                //}

                LeaveRequest Request = _mapper.Map<LeaveRequest>(RequestDTO);
                Request.EmployeeId = employee.Id;
                await _unitOfWork.LeaveRequest.Create(Request);
                // await _dbvilla.Save();
                _response.Result = _mapper.Map<LeaveRequestDTO>(Request);
                _response.StatusCode = HttpStatusCode.OK;
                // return Ok(_response);

                return CreatedAtRoute("GetRequestById", new { id = Request.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    

        [HttpPut("UpdateMyRequstes")]
        [Authorize(Roles = "Employee, Manager")]
        [HttpPut("update/{id:int}", Name = "UpdateMyRequstes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateRequest(int id, [FromBody] RequestUpdateDTO RequestDTO)
        {
            try
            {

                if (id != RequestDTO.Id || RequestDTO == null)
                    return BadRequest();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID
                var req = await _unitOfWork.LeaveRequest.Get(u => u.Id == id, false, includProperties: "Employee");
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, false);

                // Check if the leave request belongs to the logged-in employee
                if (req == null || req.EmployeeId != employee.Id)
                {
                    return BadRequest("You can only update your own leave requests.");
                }

                LeaveRequest model = _mapper.Map<LeaveRequest>(RequestDTO);

                await _unitOfWork.LeaveRequest.Update(model);

                //  _response.Result = _mapper.Map<VillaDTO>(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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
