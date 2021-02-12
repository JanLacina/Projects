using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsTransportManager.Models
{
    public class Planet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public int ShipCapacity { get; set; }
        public int ActuallyDocked { get; set; }
        public int Landing { get; set; }

        public List<Ship> ListOfShips { get; set; }

        public Planet(string name, int shipCapacity)
        {
            Name = name;

            if (shipCapacity > 1) 
                { 
                ShipCapacity = shipCapacity;
                }
            else
            {
                ShipCapacity = 1;
            }
        }
    }
}
