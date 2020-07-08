using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.Docker.WebAPI.Models
{
    public class CarUpdateModel
    { 
        [Required]
        [MaxLength(255)]
        public String Make { get; set; }
        [Required]
        [MaxLength(255)]
        public string Model { get; set; }
        [Range(75, 900)]
        public int Power { get; set; }
        [Required]
        public Transmission Transmission { get; set; }
    }
}
