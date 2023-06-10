namespace EaglesTMS.Models
{
    public abstract class BaseEntity
    {
        public int Id { get;private set; }
        public DateTime CreationDate { get; set; }
    }
}
