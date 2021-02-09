using Microsoft.AspNetCore.Mvc;
using ShipsTransportManager.DTO;
using ShipsTransportManager.Models;
using ShipsTransportManager.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


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

            return RedirectToAction("Mainpage");
        }

        [HttpGet("/undock/{id}")]
        public IActionResult UnDock(int id)
        {
            ShipService.UndockById(id);

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
        [HttpDelete("/planet/{id}")]
        public IActionResult DeletePlanet(int id)
        {
            List<Ship> GetAllOnPlanet = ShipService.GetAllOnPlanet(id);
            int moveToPlanet = PlanetService.FirstAvailable();

            if (PlanetService.IsEnoughOfFreeSpaces(GetAllOnPlanet, PlanetService.GetAvailable()))
            {
                foreach (var Ship in GetAllOnPlanet)
                {
                    ShipService.DeletePlanetMove(Ship.Id, moveToPlanet);
                    GetAllOnPlanet.Remove(Ship);
                }
            }
            else //Unfortunately, if there is no free space on any planet, then ship is destroyed with the planet ↓
            {
                foreach (var Ship in GetAllOnPlanet)
                {
                    ShipService.DeleteShip(Ship.Id);
                }
            }

            return RedirectToAction("Mainpage");
        }

        //Ultimate God thing :)
        [HttpGet("/planet/new")]
        public IActionResult CreatePlanet(string name, int shipCapacity)
        {
            PlanetService.CreatePlanet(name, shipCapacity);
            return RedirectToAction("Mainpage");
        }
    }
}
