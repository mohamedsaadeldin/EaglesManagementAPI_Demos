

namespace EaglesTMS.Models
{
    public class ApplicationUser : IdentityUser
    {

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string AddById { get; set; }
        public DateTime CreationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }

    }
}
