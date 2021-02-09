using ShipsTransportManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsTransportManager.DTO
{
    public class IndexViewDTO
    {
        public List<Ship> Ships { get; set; }
        public List<Planet> Planets { get; set; }
        public IndexViewDTO()
        {
                
        }

    }
}
