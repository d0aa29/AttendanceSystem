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

    

        [Authorize(Roles = "Employee, Manager")]
        [HttpDelete("{id:int}", Name = "DeleteRequest")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteRequest(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID
                var Request = await _unitOfWork.LeaveRequest.Get(u => u.Id == id, false, includProperties: "Employee");
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, false);

                // Check if the leave request belongs to the logged-in employee
                if (Request == null || Request.EmployeeId != employee.Id)
                {
                    return BadRequest("You can only update your own leave requests.");
                }
                // var Request = await _unitOfWork.LeaveRequest.Get(x => x.Id == id);

                if (Request == null)
                {
                    return BadRequest();
                }

                await _unitOfWork.LeaveRequest.Remove(Request);
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult<APIResponse>> GetAllRequest()
        {
            try
            {
                IEnumerable<LeaveRequest> RequestList;
                if (User.IsInRole("Admin"))
                {
                    // Admin can access all attendance records
                    RequestList = await _unitOfWork.LeaveRequest.GetAll();
                }
                else if (User.IsInRole("Manager"))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId);
                    if (employee == null)
                    {
                        return NotFound("Employee not found.");
                    }
                    RequestList = await _unitOfWork.LeaveRequest.GetAll(x => x.Employee.DepartmentId == employee.DepartmentId);
                }
                else
                {
                    return Forbid();
                }

                _response.Result = RequestList;
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

        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{id:int}", Name = "GetRequestById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRequestById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                LeaveRequest Request;

                if (User.IsInRole("Admin"))
                {
                    // Admin can access all attendance records
                    Request = await _unitOfWork.LeaveRequest.Get(x => x.Id == id);
                }
                else if (User.IsInRole("Manager"))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var Manager = await _unitOfWork.Employee.Get(u => u.UserId == userId);
                    if (Manager == null)
                    {
                        return NotFound("Employee not found.");
                    }
                    Request = await _unitOfWork.LeaveRequest.Get(x => x.Id == id && x.Employee.DepartmentId == Manager.DepartmentId);
                    if (Request == null)
                    {

                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }

                }
                else
                {
                    return Forbid();
                }

                // _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.Result = _mapper.Map<LeaveRequestDTO>(Request);
                // _response.Result = Request;
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
