using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaglesTMS.Models.DTO.JobDto
{
    public class CreateDto
    {
        public string JobName { get; set; }
        public int NumberOfAssignees { get; set; }
    }
}
