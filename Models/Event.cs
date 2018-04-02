using System;
using System.ComponentModel.DataAnnotations;

namespace LifeReelAPI.Models
{
    public class Event
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public User User {get; set;}

        [Required]
        public string Title {get; set;}

        [Required]
        public int Rating {get; set;}

        [Required]
        public DateTime Date {get; set;}

        public string Description {get; set;}

        public bool Private {get; set; } = false;
        
    }
}