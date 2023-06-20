namespace EaglesTMS.Models.DTO.NationalitiesDto
{
    public class UpdateNationlityDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(2),MinLength(2)]
        public string iso { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string nicename { get; set; }
        [Required]
        public string iso3 { get; set; }
        [Required]
        public short numcode { get; set; }
        [Required]
        public int phonecode { get; set; }
    }
}
