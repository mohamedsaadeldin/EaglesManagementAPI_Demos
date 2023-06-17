namespace EaglesTMS.Models.DTO.SensorTybeDto
{
    public class CreateDto
    {
        [Required]
        [MaxLength(20),MinLength(3)]
        public string TypeName { get; set; }
    }
}
