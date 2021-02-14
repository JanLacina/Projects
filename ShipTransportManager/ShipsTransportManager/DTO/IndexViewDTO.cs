using ShipsTransportManager.Models;
using System.Collections.Generic;

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
