using AutoMapper;
using EaglesTMS.Models;
using EaglesTMS.Models.DTO;
using EaglesTMS.Models.DTO.JobDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [HttpGet("{id:int}", Name = "GetJob")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateJob([FromBody] CreateDto createDTO)
        {

            try
            {
                //if (await _dbJobs.Jobs.GetAllAsync(u => u.JobName.ToLower() == createDTO.JobName.ToLower()) != null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "Job already Exists!");
                //    return BadRequest(ModelState);
                //}
                if (createDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Invalid Id." };
                    return BadRequest(_response);
                }
                var job = _mapper.Map<Job>(createDTO);
                await _dbJobs.Jobs.AddAsync(job);
                await _dbJobs.Jobs.SaveAsync();
                _response.Result = _mapper.Map<JobDto>(job);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetJob", new { id = job.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{id:int}", Name = "UpdateJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateJob (UpdateDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return BadRequest();
                }
                await _dbJobs.Jobs.GetFirstOrDefaultAsync(j => j.Id == updateDto.Id);
                Job job = _mapper.Map<Job>(updateDto);
                await _dbJobs.Jobs.UpdateAsync(job);
                await _dbJobs.Jobs.SaveAsync();
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
    }
}
