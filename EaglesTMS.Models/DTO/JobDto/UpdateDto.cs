using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaglesTMS.Models.DTO.JobDto
{
    public class UpdateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Job Name field is required.")]
        [MaxLength(10), MinLength(3)]
        public string JobName { get; set; }
    }
}
