using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}