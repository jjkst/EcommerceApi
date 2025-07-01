using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Contact
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Questions { get; set; } = string.Empty;
    }
}