

using EaglesTMS.Models.DTO;

namespace EaglesTMS.Utility
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<JobDto, Job>().ReverseMap();
        }
    }
}
