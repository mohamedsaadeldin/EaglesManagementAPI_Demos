namespace EaglesTMS.Models.DTO
{
    public class UpsertUser
    {
        public string Id { get; set; }

        [Required, StringLength(50,MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int CountryId { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }

        public string UserRole { get; set; }
    }
}
