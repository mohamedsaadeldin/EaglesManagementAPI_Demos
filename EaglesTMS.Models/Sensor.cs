namespace EaglesTMS.Models
{
    public class Sensor : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get ; set ; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string TypeName { get; set; }
    }
}
