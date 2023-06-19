using AutoMapper;
using EaglesTMS.Models;
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
        [HttpGet("GetAllSensor")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllSensor()
        {
            try
            {
                IEnumerable<Sensor> sensors = await _unitOfWork.Sensors.GetAllAsync(x => x.IsDeleted == false);
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

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Search(string searchQuery)
        {
            try
            {
                IEnumerable<Sensor> sensors;

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    sensors = await _unitOfWork.Sensors.GetAllAsync(x => x.IsDeleted == false && x.TypeName.Contains(searchQuery));
                }
                else
                {
                    sensors = await _unitOfWork.Sensors.GetAllAsync(x => x.IsDeleted == false);
                }

                if (!sensors.Any())
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "No Jobs Found." };
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

        [HttpGet("IsDeleted")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllSensorDeleted()
        {
            try
            {
                IEnumerable<Sensor> sensors = await _unitOfWork.Sensors.GetAllAsync(x => x.IsDeleted == true);
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

        [HttpGet("GetJob", Name = "GetSensor")]
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

        [HttpPost("AddSensor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateSensor([FromBody] CreateSensorDto model)
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
                var sens = _mapper.Map<Sensor>(model);
                await _unitOfWork.Sensors.AddAsync(sens);
                await _unitOfWork.Jobs.SaveAsync();
                _response.Result = _mapper.Map<CreateSensorDto>(sens);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSensor", new { id = sens.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("UpdateSensor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateSensor(int id, [FromBody] UpdateSensorDto model)
        {
            try
            {
                if (model == null || id != model.Id)
                {
                    ModelState.AddModelError("ErrorMessages", "Sensor ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }
                Sensor sensor = _mapper.Map<Sensor>(model);
                await _unitOfWork.Sensors.UpdateSensorAsync(sensor);
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

        [HttpPut("DeleteSensor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteSensor(int id)
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
                var deleted = await _unitOfWork.Sensors.GetFirstOrDefaultAsync(x => x.Id == id);
                if (deleted == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "ID Not Found." };
                    return BadRequest(_response);
                }
                Sensor sensor = _mapper.Map<Sensor>(deleted);
                await _unitOfWork.Sensors.DeleteSensorAsync(sensor);
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

        [HttpPut("RestoreDeletedSensor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> RestoreDeletedSensor(int id)
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
                var deleted = await _unitOfWork.Jobs.GetFirstOrDefaultAsync(x => x.Id == id);
                if (deleted == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "ID Not Found." };
                    return BadRequest(_response);
                }
                Sensor sensor = _mapper.Map<Sensor>(deleted);
                await _unitOfWork.Sensors.RestoreSensorAsync(sensor);
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
