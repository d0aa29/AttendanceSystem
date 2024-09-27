using System.Net;
using Microsoft.AspNetCore.Mvc;
using AttendanceSystem.Models;
//using AttendanceSystem.Repository.IRepository;
using AttendanceSystem.Models.DTO;
using AttendanceSystem.Repositoty.IRepository;
using Microsoft.AspNetCore.Identity;
using Repository.IRepository;
using AutoMapper;
using Azure;
using Repository;
using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Models.DTO;
using AttendanceSystem.Repositoty.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;
using System.Net;
namespace AttendanceSystem.Controllers
{
    [Route("api/Users")]

    // [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
    

        protected APIResponse _response;
        private readonly IMapper _mapper;
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllApplicationUser()
        {
            try
            {
                IEnumerable<ApplicationUser> ApplicationUserList = await _unitOfWork.User.GetAll();
                _response.Result = ApplicationUserList;
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
        [HttpGet("{id}", Name = "GetApplicationUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetApplicationUserById(string id)
        {
            try
            {
                if (id == null)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var user = await _unitOfWork.User.Get(x => x.Id == id);
                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                // _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.Result = user;
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

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> CreateApplicationUser([FromBody] ApplicationUser DeptDTO)
        //{
        //    try
        //    {
        //        if (DeptDTO == null)
        //            return BadRequest();

        //        if (await _unitOfWork.User.Get(x => x.UserName.ToLower() == DeptDTO.UserName.ToLower()) != null)
        //        {
        //            ModelState.AddModelError("", "ApplicationUser alredy exist");
        //            return BadRequest(ModelState);
        //        }

        //        ApplicationUser Dept = _mapper.Map<ApplicationUser>(DeptDTO);

        //        await _unitOfWork.User.Create(Dept);
        //        // await _dbvilla.Save();
        //        _response.Result = _mapper.Map<ApplicationUser>(Dept);
        //        _response.StatusCode = HttpStatusCode.OK;
        //        // return Ok(_response);

        //        return CreatedAtRoute("GetApplicationUserById", new { id = Dept.Id }, _response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}

        [HttpDelete("{id}", Name = "DeleteApplicationUser")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteApplicationUser(string id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var user = await _unitOfWork.User.Get(x => x.Id == id);

                if (user == null)
                {
                    return BadRequest();
                }

                await _unitOfWork.User.Remove(user);
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


        [HttpPut("{id}", Name = "UpdateApplicationUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateApplicationUser(string id, [FromBody] UserUpdateDTO userDTO)
        {
            try
            {
                if (id != userDTO.Id || userDTO == null)
                    return BadRequest();

                var existingUser = await _unitOfWork.User.Get(x => x.Id == id);
                if (existingUser == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _mapper.Map(userDTO, existingUser);
                await _unitOfWork.User.Update(existingUser);
                _response.Result = _mapper.Map<UserUpdateDTO>(existingUser);

                //ApplicationUser model = _mapper.Map<ApplicationUser>(userDTO);
                //await _unitOfWork.User.Update(model);
                //_response.Result = _mapper.Map<UserUpdateDTO>(model);
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
