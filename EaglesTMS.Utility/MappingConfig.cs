

using EaglesTMS.Models.DTO;
using EaglesTMS.Models.DTO.JobDto;
using EaglesTMS.Models.DTO.SensorTybeDto;

namespace EaglesTMS.Utility
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<JobDto, Job>().ReverseMap();
            CreateMap<Models.DTO.JobDto.CreateDto, Job>().ReverseMap();
            CreateMap<UpdateDto, Job>().ReverseMap();
            CreateMap<Sensor, SensorDto>().ReverseMap();
            CreateMap<Sensor, Models.DTO.SensorTybeDto.CreateDto>().ReverseMap();


        }
    }
}
