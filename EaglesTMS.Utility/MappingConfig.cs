

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
            CreateMap<CreateJobDto, Job>().ReverseMap();
            CreateMap<UpdateSensorDto, Job>().ReverseMap();
            CreateMap<DeleteJobDto, Job>().ReverseMap();

            CreateMap<SensorDto, Sensor>().ReverseMap();
            CreateMap<CreateSensorDto, Sensor>().ReverseMap();
            CreateMap<UpdateSensorDto, Sensor>().ReverseMap();
        }
    }
}
