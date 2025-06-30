using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Contact
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Questions { get; set; }
    }
}