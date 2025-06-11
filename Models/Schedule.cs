using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        public string Service { get; set; }

        [Required]
        public DateTime SelectedDate { get; set; }

        [Required]
        public string Timeslot { get; set; }

        public string Note { get; set; }

        [Required]
        public string Uid { get; set; }
    }
}