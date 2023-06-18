namespace EaglesTMS.Models.DTO.SensorTybeDto
{
    public class CreateSensorDto
    {
        [Required]
        [MaxLength(20),MinLength(3)]
        public string TypeName { get; set; }
    }
}
