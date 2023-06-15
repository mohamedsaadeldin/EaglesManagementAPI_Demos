

namespace EaglesTMS.Models
{
    public class Job : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string JobName { get; set; }
        public int NumberOfAss { get; set; }
        public DateTime CreationDate { get ; set; }
    }
}
