using AttendanceSystem.Models.DTO;
using AttendanceSystem.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AttendanceSystem.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly APIResponse _response;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _response = new();
            _mapper = mapper;
        }
        [HttpGet("Myprofile")]
        [Authorize(Roles = "Employee, Admin, Manager")]
        public async Task<ActionResult<APIResponse>> GetEmployeeProfile()
        {
            try
            {
                var userId =User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, includProperties: "Department,User,Shifts");

                if (employee == null)
                {
                    return NotFound();
                }

                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                _response.Result = employeeDTO;
             
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
        [HttpPut("EditMyprofile", Name = "EditMyProfile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> EditMyProfile( [FromBody] EmpEditProfileDTO empDTO)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, includProperties: "Department,User,Shifts");

                if (employee == null)
                {
                    return NotFound();
                }

                if(empDTO.DepartmentId==0)
                    empDTO.DepartmentId =employee.DepartmentId;
                else
                {
                    var departmentExists = await _unitOfWork.Department.Get(d => d.Id == empDTO.DepartmentId.Value);
                    if (departmentExists == null)
                    {
                        return BadRequest(new APIResponse { IsSuccess = false, ErrorMessages = new List<string> { "Invalid DepartmentId provided." } });
                    }
                }
              
                if ( empDTO == null)
                    return BadRequest();
               
                _mapper.Map(empDTO, employee);  // This will update only the mapped properties in the existing employee object without creating a new one

                // Ensure that the UserId is not overwritten
                employee.UserId = userId; // Just to be sure
             //  employee.Id= empDTO.Id;


                await _unitOfWork.Employee.Update(employee);

                _response.Result = _mapper.Map<EmployeeUpdateDTO>(employee);
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

        [HttpPut("EditMyShifts", Name = "EditMyShifts")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> EditMyShifts([FromBody] EmpShiftsUpdateDTO shiftUpdateDTO)
        {
            try
            {              
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get logged-in employee's ID
                var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId, includProperties: "Department,User,Shifts");

                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                if (shiftUpdateDTO == null || shiftUpdateDTO.ShiftIds == null || !shiftUpdateDTO.ShiftIds.Any())
                {
                    return BadRequest("Invalid shift data provided.");
                }

                // Fetch shifts to validate that they exist
                var shifts = await _unitOfWork.Shift.GetAll(u => shiftUpdateDTO.ShiftIds.Contains(u.Id));

                if (shifts.Count() != shiftUpdateDTO.ShiftIds.Count)
                {
                    return BadRequest("One or more shifts are invalid.");
                }

                // Clear current shifts and assign the new shifts
                employee.Shifts.Clear();
                employee.Shifts = shifts.ToList();

                // Update employee with new shifts
                await _unitOfWork.Employee.Update(employee);
               
                return NoContent();
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
        public async Task<ActionResult<APIResponse>> GetAllEmployee()
        {
            try
            {
                IEnumerable<Employee> EmployeeList;
                if (User.IsInRole("Admin"))
                {
                    // Admin can access all attendance records
                    EmployeeList = await _unitOfWork.Employee.GetAll(includProperties: "Department,User,Shifts");
                }
                else if (User.IsInRole("Manager"))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId);
                    if (employee == null)
                    {
                        return NotFound("Employee not found.");
                    }

                    EmployeeList = await _unitOfWork.Employee.GetAll(x => x.DepartmentId == employee.DepartmentId, includProperties: "Department,User,Shifts");
                }
                else
                {
                    return Forbid();
                }
               
                var employeeDTOList = EmployeeList.Select(emp => new EmployeeDTO
                {
                    Id = emp.Id,
                    SSN = emp.SSN,
                    Name = emp.Name,
                    Gender = emp.Gender,
                    BirthDate = emp.BirthDate,
                    JoinedOn = emp.JoinedOn,
                    DepartmentName = emp.Department != null ? emp.Department.Name : "No Department",  // Handle null department
                    UserName = emp.User != null ? emp.User.UserName : "No User",  // Handle null user
                    Shifts = emp.Shifts != null
                   ? emp.Shifts.Select(shift => new ShiftDTO {Num=shift.Num, From = shift.From, To = shift.To }).ToList()
                    : new List<ShiftDTO>()
                }).ToList();

                _response.Result = employeeDTOList;
              //  _response.Result = EmployeeList;
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
        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEmployeeById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                Employee emp;
                if (User.IsInRole("Admin"))
                {
                    // Admin can access all attendance records
                    emp = await _unitOfWork.Employee.Get(x => x.Id == id, includProperties: "Department,User,Shifts");
                }
                else if (User.IsInRole("Manager"))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId);
                    if (employee == null)
                    {
                        return NotFound("Employee not found.");
                    }

                    emp = await _unitOfWork.Employee.Get(x => x.Id == id && x.DepartmentId == employee.DepartmentId  , includProperties: "Department,User,Shifts");
                    if (emp == null)
                    {
                        return NotFound("Employee not found.");
                    }

                }
                else
                {
                    return Forbid();
                }

             
                if (emp == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                var employeeDTO = new EmployeeDTO
                {
                    Id = emp.Id,
                    SSN = emp.SSN,
                    Name = emp.Name,
                    Gender = emp.Gender,
                    BirthDate = emp.BirthDate,
                    JoinedOn = emp.JoinedOn,
                    DepartmentName = emp.Department.Name,
                    UserName = emp.User.UserName
                };

                _response.Result = employeeDTO;
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
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeCreateDTO empDTO)
        {
            try
            {
                if (empDTO == null)
                    return BadRequest();
                if (User.IsInRole("Manager"))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var employee = await _unitOfWork.Employee.Get(u => u.UserId == userId);

                    if (employee == null)
                    {
                        return NotFound("Employee not found.");
                    }

                    empDTO.DepartmentId = employee.DepartmentId;
                }
                if (await _unitOfWork.Employee.Get(x => x.SSN == empDTO.SSN) != null)
                {
                    ModelState.AddModelError("", "Employee alredy exist");
                    return BadRequest(ModelState);
                }

                Employee emp = _mapper.Map<Employee>(empDTO);
                emp.DepartmentId = empDTO.DepartmentId;
                await _unitOfWork.Employee.Create(emp);
                // await _dbvilla.Save();
                _response.Result = _mapper.Map<EmployeeCreateDTO>(emp);
                _response.StatusCode = HttpStatusCode.OK;
                // return Ok(_response);

                return CreatedAtRoute("GetEmployeeById", new { id = emp.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteEmployee(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var emp = await _unitOfWork.Employee.Get(x => x.Id == id);
                if (emp == null)
                {
                    return BadRequest();
                }
                if (User.IsInRole("Manager"))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var manager = await _unitOfWork.Employee.Get(u => u.UserId == userId);

                    if (manager == null) 
                    {
                        return NotFound("Employee not found.");
                    }
                    if(manager.DepartmentId != emp.DepartmentId)
                        return NotFound("You are not authorized to delete this employee.");

                }
                

                await _unitOfWork.Employee.Remove(emp);
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

        [HttpPut("{id:int}", Name = "UpdateEmployee")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO empDTO)
        {
            try
            {
                if (id != empDTO.Id || empDTO == null)
                    return BadRequest();

                var emp = await _unitOfWork.Employee.Get(x => x.Id == id);
               
                if (emp == null)
                {
                    return BadRequest();
                }
               
                if (User.IsInRole("Manager"))
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var manager = await _unitOfWork.Employee.Get(u => u.UserId == userId);

                    if (manager == null || manager.DepartmentId != emp.DepartmentId)
                    {
                        return NotFound("Employee not found.");
                    }

                    empDTO.DepartmentId = emp.DepartmentId;
                }
               

                Employee model = _mapper.Map<Employee>(empDTO);


                await _unitOfWork.Employee.Update(model);

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
