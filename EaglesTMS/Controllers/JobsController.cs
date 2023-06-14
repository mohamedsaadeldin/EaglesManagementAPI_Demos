using AutoMapper;
using EaglesTMS.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EaglesTMS.Controllers
{
    [Route("api/Jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _dbJobs;
        private readonly IMapper _mapper;
        public JobsController(IUnitOfWork dbJobs ,IMapper mapper)
        {
            _dbJobs = dbJobs;
            _response = new APIResponse();
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetAllJobs()
        {
            try
            {
                IEnumerable<Job> jobs =  await _dbJobs.Jobs.GetAllAsync();
                if (jobs.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "No Jobs Found." };
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<List<JobDto>>(jobs);
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
        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<APIResponse>> GetJob(int id)
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
                var job = await _dbJobs.Jobs.GetFirstOrDefaultAsync(x => x.Id == id);
                if (job == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "ID Not Found." };
                    return BadRequest(_response);
                }
                _response.Result = _mapper.Map<JobDto>(job);
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
    }
}
