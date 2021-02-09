using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsTransportManager.Models
{
    public class Ship
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }
        [Required]
        public double WarpSpeed { get; set; }

        [Required]
        public Planet Planet { get; set; }
        public bool IsDocked { get; set; }

        public Ship(String name, double warpSpeed, Planet planet)
        {
            Name = name;
            WarpSpeed = warpSpeed;
            Planet = planet;
            IsDocked = false;
        }
        public Ship()
        {
        }
    }
}
