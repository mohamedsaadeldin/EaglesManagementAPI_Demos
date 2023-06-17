using AutoMapper;
using EaglesTMS.Models.DTO.JobDto;
using EaglesTMS.Models.DTO.SensorTybeDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EaglesTMS.Controllers
{
    [Route("api/Sensors")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SensorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllSensor()
        {
            try
            {
                IEnumerable<Sensor> sensors = await _unitOfWork.Sensors.GetAllAsync();
                if (sensors.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "No Sensor Found." };
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<List<SensorDto>>(sensors);
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

        [HttpGet("{id:int}", Name = "GetSensor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetSensor(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Invalid Id." };
                    return BadRequest(_response);
                }
                var sensor = await _unitOfWork.Sensors.GetFirstOrDefaultAsync(x => x.Id == id);
                if (sensor == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "ID Not Found." };
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<SensorDto>(sensor);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateSensor([FromBody] Models.DTO.SensorTybeDto.CreateDto model)
        {
            try
            {
                if (await _unitOfWork.Sensors.GetAsync(u => u.TypeName.ToLower() == model.TypeName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Sensor already Exists!");
                    return BadRequest(ModelState);
                }
                if (model == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Invalid Id." };
                    return BadRequest(_response);
                }
                var sensor = _mapper.Map<Sensor>(model);
                await _unitOfWork.Sensors.AddAsync(sensor);
                await _unitOfWork.Sensors.SaveAsync();
                _response.Result = _mapper.Map<Sensor>(model);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSensor", new { id = sensor.Id }, _response);
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
