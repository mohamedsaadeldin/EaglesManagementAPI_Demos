using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaglesTMS.Models
{
    public class Job : IBaseEntity
    {
        public int Id { get; set; }
        public string JobName { get; set; }
        public DateTime CreationDate { get ; set; }
    }
}
