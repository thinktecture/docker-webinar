using System;

namespace Thinktecture.Docker.WebAPI.Models
{
    public class CarDetailsModel
    {
        public Guid Id { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public int Power { get; set; }
        public Transmission Transmission { get; set; }
    }
}
