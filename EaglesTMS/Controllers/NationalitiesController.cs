using AutoMapper;
using EaglesTMS.Models.DTO.NationalitiesDto;
using EaglesTMS.Models.DTO.SensorTybeDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Net;

namespace EaglesTMS.Controllers
{
    [Route("api/Nationalities")]
    [ApiController]
    public class NationalitiesController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NationalitiesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _response = new APIResponse();
            _mapper = mapper;
        }
        [HttpGet("GetAllNationalities")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllNationalities()
        {
            try
            {
                IEnumerable<Nationalities> nationalities = await _unitOfWork.Nationalities.GetAllAsync();
                _response.Result = _mapper.Map<List<NationalityDto>>(nationalities);
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
                IEnumerable<Nationalities> nationalities;
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    nationalities = await _unitOfWork.Nationalities.GetAllAsync(
                        x => x.iso.Contains(searchQuery) || x.name.Contains(searchQuery)
                    );
                }
                else
                {
                    nationalities = await _unitOfWork.Nationalities.GetAllAsync();
                }

                if (!nationalities.Any())
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "No Nationl Found." };
                    return BadRequest(_response);
                }

                _response.Result = _mapper.Map<List<NationalityDto>>(nationalities);
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

        [HttpGet("GetNationality", Name = "GetNationality")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNational(int id)
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
                var national = await _unitOfWork.Nationalities.GetFirstOrDefaultAsync(x => x.Id == id);
                if (national == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "National Not Found." };
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<NationalityDto>(national);
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

        [HttpPost("AddNationality")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNationality([FromBody] CreateNationalityDto model)
        {
            try
            {
                if (await _unitOfWork.Nationalities.GetAsync(u => u.name.ToLower() == model.name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Nationality already Exists!");
                    return BadRequest(ModelState);
                }
                if (model == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Invalid Id." };
                    return BadRequest(_response);
                }
                var national = _mapper.Map<Nationalities>(model);
                await _unitOfWork.Nationalities.AddAsync(national);
                await _unitOfWork.Nationalities.SaveAsync();
                _response.Result = _mapper.Map<CreateNationalityDto>(national);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetNationality", new { id = national.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteNationality")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteNationality(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var national = await _unitOfWork.Nationalities.GetFirstOrDefaultAsync(u => u.Id == id);
                if (national == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Nationalities.RemoveAsync(national);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
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

        [HttpPut("UpdateNationality")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateNationality(int id, [FromBody] UpdateNationlityDto model)
        {
            try
            {
                if (model == null || id != model.Id)
                {
                    ModelState.AddModelError("ErrorMessages", "National ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }
                Nationalities nationalities = _mapper.Map<Nationalities>(model);
                await _unitOfWork.Nationalities.UpdateNationalityAsync(nationalities);
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
