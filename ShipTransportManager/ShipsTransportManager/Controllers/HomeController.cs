using Microsoft.AspNetCore.Mvc;
using ShipsTransportManager.DTO;
using ShipsTransportManager.Models;
using ShipsTransportManager.Services;
using System.Collections.Generic;
using System.Linq;


namespace ShipsTransportManager.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private ShipService ShipService { get; set; }
        private PlanetService PlanetService { get; set; }


        public HomeController(ShipService shipService, PlanetService planetService)
        {
            ShipService = shipService;
            PlanetService = planetService;
        }


        [HttpGet("")]
        public IActionResult Mainpage()
        {
            var Model = new IndexViewDTO
            {
                Ships = ShipService.GetAll(),
                Planets = PlanetService.GetAll(),
            };

            return View("Index", Model);
        }

        [HttpPost("/create/")]
        public IActionResult Create(string name, int warpSpeed, int planetId)
        {
            ShipService.CreateShip(name, warpSpeed, planetId);

            return RedirectToAction("Mainpage");
        }

        [HttpPost("/move/")]
        public IActionResult Move(int id, int planetId)
        {
            ShipService.Move(id, planetId);

            return RedirectToAction("Mainpage");
        }

        [HttpGet("/dock/{id}")]
        public IActionResult Dock(int id)
        {
            ShipService.DockByID(id);
            PlanetService.ShipDock(id);

            return RedirectToAction("Mainpage");
        }

        [HttpGet("/undock/{id}")]
        public IActionResult UnDock(int id)
        {
            ShipService.UndockById(id);
            PlanetService.ShipUndock(id);

            return RedirectToAction("Mainpage");
        }

        /*
        Get ships with specific rules (all docked their, ...)
        */
        [HttpGet("test")]
        public IActionResult Tesst()
        {

            
            return RedirectToAction("Mainpage");
        }

        //Otestovat
        [HttpGet("/planet/delete/{id}")]
        public IActionResult DeletePlanet(int id)
        {
            List<Ship> GetAllOnPlanet = ShipService.GetAllOnPlanet(id);
            int moveToPlanet = PlanetService.FirstAvailable();
            int freeSpaces = PlanetService.CountOfFreeSpaces(GetAllOnPlanet, PlanetService.GetAvailable());

            foreach (var Ship in GetAllOnPlanet.ToList()) //Great trick to remove objects from iterated list
            {
                if(freeSpaces > 0) 
                { 
                    if(Ship.IsDocked == true) 
                    {
                        ShipService.UndockById(Ship.Id);
                    }
                    ShipService.Move(Ship.Id, moveToPlanet);
                    GetAllOnPlanet.Remove(Ship);
                    freeSpaces--;
                }
            }
            PlanetService.RemoveById(id);

            //Unfortunately, if there is no free space on any planet, then ship is destroyed with the planet ↓            
            foreach (var Ship in GetAllOnPlanet)
            {
                ShipService.DeleteShip(Ship);
                PlanetService.RemoveById(id);
            }

            return RedirectToAction("Mainpage");
        }

        //Ultimate God thing :)
        [HttpGet("planet/new")]
        public IActionResult CreatePlanet()
        {
            return View("PlanetNew");
        }

        [HttpPost("/planet/new")]
        public IActionResult CreatePlanet(string name, int shipCapacity)
        {
            PlanetService.CreatePlanet(name, shipCapacity);
            return RedirectToAction("Mainpage");
        }
    }
}
