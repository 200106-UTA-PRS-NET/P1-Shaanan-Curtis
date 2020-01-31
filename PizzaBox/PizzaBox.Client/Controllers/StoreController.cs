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
        private readonly Models.Assets OrderAssets = new Models.Assets();
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
            OrderAssets.Stores = stores;
            return View(OrderAssets);
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