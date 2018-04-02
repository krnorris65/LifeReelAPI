using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace LifeReelAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName {get; set;}

        [Required]
        public string LastName {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated {get; set;}

    }
}