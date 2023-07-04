namespace EaglesTMS.Controllers
{
    [Route("api/Jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public JobsController(IUnitOfWork dbJobs ,IMapper mapper)
        {
            _unitOfWork = dbJobs;
            _response = new APIResponse();
            _mapper = mapper;
        }

        [HttpGet("AllJob")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllJobs()
        {
            try
            {
                IEnumerable<Job> jobs = await _unitOfWork.Jobs.GetAllAsync(x => x.IsDeleted == false);
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

        [HttpGet("Search")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Search(string searchQuery)
        {
            try
            {
                IEnumerable<Job> jobs;

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    jobs = await _unitOfWork.Jobs.GetAllAsync(x => x.IsDeleted == false && x.JobName.Contains(searchQuery));
                }
                else
                {
                    jobs = await _unitOfWork.Jobs.GetAllAsync(x => x.IsDeleted == false);
                }

                if (!jobs.Any())
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

        [HttpGet("IsDeleted")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAllJobDeleted()
        {
            try
            {
                IEnumerable<Job> jobs =  await _unitOfWork.Jobs.GetAllAsync(x=>x.IsDeleted == true);
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

        [HttpGet("GetJob", Name = "GetJob")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
                var job = await _unitOfWork.Jobs.GetFirstOrDefaultAsync(x => x.Id == id);
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

        [HttpPost("AddJob")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateJob([FromBody] CreateJobDto createDTO)
        {
            try
            {
                if (await _unitOfWork.Jobs.GetAsync(u => u.JobName.ToLower() == createDTO.JobName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Job already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "Invalid Id." };
                    return BadRequest(_response);
                }
                var job = _mapper.Map<Job>(createDTO);
                await _unitOfWork.Jobs.AddAsync(job);
                await _unitOfWork.Jobs.SaveAsync();
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

        [HttpPut("UpdateJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateJob(int id, [FromBody] UpdateJobDto model)
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
                Job job = _mapper.Map<Job>(model);
                await _unitOfWork.Jobs.UpdateJobAsync(job);
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

        [HttpPut("DeletedJob")]
        public async Task<ActionResult<APIResponse>> DeleteJob(int id)
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
                Job job = _mapper.Map<Job>(deleted);
                await _unitOfWork.Jobs.DeleteJobAsync(job);
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

        [HttpPut("RestoreDeletedJob")]
        public async Task<ActionResult<APIResponse>> RestoreDeleteJob(int id)
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
                var restore = await _unitOfWork.Jobs.GetFirstOrDefaultAsync(x => x.Id == id);
                if (restore == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string>() { "ID Not Found." };
                    return BadRequest(_response);
                }
                Job job = _mapper.Map<Job>(restore);
                await _unitOfWork.Jobs.RestoreJobAsync(job);
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
