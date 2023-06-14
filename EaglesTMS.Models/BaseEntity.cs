namespace EaglesTMS.Models
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
