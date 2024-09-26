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
    [Route("api/Shift")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly APIResponse _response;
        private readonly IMapper _mapper;
        public ShiftController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _response = new();
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllShift()
        {
            try
            {
                IEnumerable<Shift> ShiftList = await _unitOfWork.Shift.GetAll();
                _response.Result = ShiftList;
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
        [HttpGet("{id:int}", Name = "GetShiftById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetShiftById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var shift = await _unitOfWork.Shift.Get(x => x.Id == id);
                if (shift == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                // _response.Result = _mapper.Map<VillaDTO>(villa);
                _response.Result = shift;
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
        public async Task<ActionResult<APIResponse>> CreateShift([FromBody] ShiftDTO shiftDTO)
        {
            try
            {
                if (shiftDTO == null)
                    return BadRequest();

                if (await  _unitOfWork.Shift.Get(x => x.Num == shiftDTO.Num) != null)
                {
                    ModelState.AddModelError("", "Shift alredy exist");
                    return BadRequest(ModelState);
                }

                Shift shift = _mapper.Map<Shift>(shiftDTO);

                await _unitOfWork.Shift.Create(shift);
                // await _dbvilla.Save();
                _response.Result = _mapper.Map<ShiftDTO>(shift);
                _response.StatusCode = HttpStatusCode.OK;
                // return Ok(_response);

                return CreatedAtRoute("GetShiftById", new {id= shift.Id}, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteShift")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteShift(int id)
        {
            try
            {
                if (id == null)
                    return BadRequest();
                var shift = await _unitOfWork.Shift.Get(x => x.Id == id);

                if (shift == null)
                {
                    return BadRequest();
                }

                await _unitOfWork.Shift.Remove(shift);
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

        [HttpPut("{id:int}", Name = "UpdateShift")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateShift(int id, [FromBody] ShiftUpdateDTO shiftDTO)
        {
            try
            {
                

                if (id != shiftDTO.Id || shiftDTO == null)
                    return BadRequest();

                Shift model = _mapper.Map<Shift>(shiftDTO);

                await _unitOfWork.Shift.Update(model);

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
