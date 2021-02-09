using ShipsTransportManager.Context;
using ShipsTransportManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsTransportManager.Services
{
    public class PlanetService
    {
        private readonly STMContext db;

        public PlanetService(STMContext db)
        {
            this.db = db;
        }

        public List<Planet> GetAll()
        {
            return db.Planets.ToList();
        }

        public void CreatePlanet(string name, int shipCapacity)
        {
            db.Add(new Planet(name, shipCapacity));
            db.SaveChanges();
        }
        
        public Dictionary<Planet, int> GetAvailable()
        {
            Dictionary<Planet, int> Available = new Dictionary<Planet, int>();

            List<Planet> Planets = db.Planets.Where(p => p.ShipCapacity-p.ActuallyDocked > 0).ToList();

            foreach (var Planet in Planets)
            {
                Available.Add(Planet, Planet.ShipCapacity-Planet.ActuallyDocked);
            }

            return Available;
        }

        public int FirstAvailable()
        {  
            return db.Planets.Where(p => p.ShipCapacity - p.ActuallyDocked > 0).First().Id;
        }

        public Planet GetById(int id)
        {
            return db.Planets.FirstOrDefault(Planet => Planet.Id == id);
        }

        public void RemoveById(int id)
        {

            db.Remove(GetById(id));
            db.SaveChanges();
        }

        public bool IsEnoughOfFreeSpaces(List<Ship> ShipsOnPlanet, Dictionary<Planet,int> AvailablePlanets)
        {
            int availableSpaces = 0;

            foreach (var item in AvailablePlanets)
            {
                availableSpaces += item.Value;
            }

            return availableSpaces > ShipsOnPlanet.Count();
        }
    }
}
