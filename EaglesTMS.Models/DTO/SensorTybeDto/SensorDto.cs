namespace EaglesTMS.Models.DTO.SensorTybeDto
{
    public class SensorDto
    {
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
