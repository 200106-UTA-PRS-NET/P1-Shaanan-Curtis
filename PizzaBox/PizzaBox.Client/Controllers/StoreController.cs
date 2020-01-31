using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing.Interfaces;

namespace PizzaBox.Client.Controllers
{
    public class StoreController : Controller
    {
        //Dependency Injection
        private readonly IPizzaBoxRepository _PBrepository;
        private readonly Models.OrderAssets _OrderAssets = new Models.OrderAssets();
        public StoreController(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
        }

        /*
        // GET: /Store/
        public IActionResult Index(string name)
        {
            //ViewData["message"] = "Hello " + name;
            return View();
        }
        */

        public IActionResult Order()
        {
            //pass locations, orders, ordertype
            var stores = _PBrepository.GetAllStores();
            _OrderAssets.Stores = stores;
            return View(_OrderAssets);
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Locations()
        {
            var locations = _PBrepository.GetAllStores();
            return View(locations);
        }
    }
}