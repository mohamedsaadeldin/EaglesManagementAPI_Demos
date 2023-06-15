

using EaglesTMS.Models.DTO;
using EaglesTMS.Models.DTO.JobDto;

namespace EaglesTMS.Utility
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<JobDto, Job>().ReverseMap();
            CreateMap<CreateDto, Job>().ReverseMap();
        }
    }
}
