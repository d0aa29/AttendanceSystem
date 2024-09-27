using AttendanceSystem.Models.DTO;
using AttendanceSystem.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Net;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllEmployee()
        {
            try
            {
                IEnumerable<Employee> EmployeeList = await _unitOfWork.Employee.GetAll();
                _response.Result = EmployeeList;
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
                var emp = await _unitOfWork.Employee.Get(x => x.Id == id);
                if (emp == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                // _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.Result = emp;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeCreateDTO empDTO)
        {
            try
            {
                if (empDTO == null)
                    return BadRequest();

                if (await _unitOfWork.Employee.Get(x => x.SSN == empDTO.SSN) != null)
                {
                    ModelState.AddModelError("", "Employee alredy exist");
                    return BadRequest(ModelState);
                }

                Employee emp = _mapper.Map<Employee>(empDTO);

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO empDTO)
        {
            try
            {
                if (id != empDTO.Id || empDTO == null)
                    return BadRequest();


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
