using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaglesTMS.Models.DTO.JobDto
{
    public class CreateJobDto
    {
        [Required(ErrorMessage = "Job Name field is required.")]
        [MaxLength(10), MinLength(2)]
        public string JobName { get; set; }
        public int NumberOfAss { get; set; }
    }
}
