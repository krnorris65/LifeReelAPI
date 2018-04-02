using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LifeReelAPI.Models
{
    public class Friend
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public User Sender {get; set;}

        [Required]
        public User Receiver {get; set;}

        public bool Pending {get; set;} = true;

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Date {get; set;}
    }
}