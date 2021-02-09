using Microsoft.EntityFrameworkCore;
using ShipsTransportManager.Context;
using ShipsTransportManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipsTransportManager.Services
{
    public class ShipService
    {

        public readonly STMContext db;
        public ShipService(STMContext db)
        {
            this.db = db;
        }

        public List<Ship> GetAll()
        {
            return db.Ships.Include(s=>s.Planet).ToList();
        }

        public List<Ship> GetAllOnPlanet(int planetId)
        {
            return db.Ships.Include(s => s.Planet).Where(s => s.Planet.Id == planetId).ToList();
        }

        public Ship GetByName(string name)
        {
            return db.Ships.Include(s => s.Planet).FirstOrDefault(ship => ship.Name == name);
        }
        public Ship GetById(int id)
        {
            return db.Ships.Include(s => s.Planet).FirstOrDefault(ship => ship.Id == id);
        }

        public Ship CreateShip(string name, double warpSpeed, int planetId)
        {
            Planet planet = db.Planets.FirstOrDefault(p => p.Id == planetId);
            if (planet.ShipCapacity > planet.ActuallyDocked)
            {
                var result = db.Ships.Add(new Ship(name, warpSpeed, planet)).Entity;
                planet.ActuallyDocked += 1;
                db.SaveChanges();

                return result;
            }
            return null;
        }

        public void Move (int id, int planetId)
        {
            Ship toMove = GetById(id);

            Planet oldPlanet = toMove.Planet;
            Planet newPlanet = db.Planets.FirstOrDefault(p => p.Id == planetId);

            if (newPlanet.ShipCapacity > newPlanet.ActuallyDocked) { 
                if (toMove.IsDocked == false)
                {
                    //Ship update
                    toMove.Planet = newPlanet;
                    db.Update(toMove);

                    //Planet update
                    newPlanet.ActuallyDocked += 1;
                    oldPlanet.ActuallyDocked -= 1;

                    db.Update(oldPlanet);
                    db.Update(newPlanet);

                    db.SaveChanges();
                }
            }
        }
        //Only difference is IsDocked is no longer important
        public void DeletePlanetMove(int id, int planetId)
        {
            Ship toMove = GetById(id);

            Planet oldPlanet = toMove.Planet;
            Planet newPlanet = db.Planets.FirstOrDefault(p => p.Id == planetId);

            if (newPlanet.ShipCapacity > newPlanet.ActuallyDocked)
            {
                //Ship update
                toMove.Planet = newPlanet;
                db.Update(toMove);
                
                //Planet update
                newPlanet.ActuallyDocked += 1;
                oldPlanet.ActuallyDocked -= 1;
                
                db.Update(oldPlanet);
                db.Update(newPlanet);
                
                db.SaveChanges();
            }
        }


        public List<Ship> GetWarpAbove(double wantedWarp)
        {
            List<Ship> warpAbove = GetAll()
                .Where(ship => ship.WarpSpeed >= wantedWarp)
                .OrderByDescending(ship => ship.WarpSpeed)
                .ToList();

            return warpAbove;
        }
        public void DockByID(int id)
        {
            var ToDock = GetById(id);

            ToDock.IsDocked = true;
            db.Update(ToDock);
            db.SaveChanges();
        }

        public void UndockById(int id)
        {
            var ToUnDock = GetById(id);

            ToUnDock.IsDocked = false;
            db.Update(ToUnDock);
            db.SaveChanges();
        }

        public void DeleteShip (int id)
        {
            db.Remove(id);
            db.SaveChanges();
        }
    }
}
