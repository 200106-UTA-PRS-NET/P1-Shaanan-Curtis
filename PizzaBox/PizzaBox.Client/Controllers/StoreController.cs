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
            if(Models.Assets.Session == false)
            {
                return Redirect("~/Home/Signin");
            }

            //can only order from 1 location every 24 hours
            if(Models.Assets.today != DateTime.MinValue && Models.Assets.tomorrow != DateTime.MinValue)
            {
                if (System.DateTime.Compare(Models.Assets.today, Models.Assets.tomorrow) < 0)
                {
                    return RedirectToAction("Step1", "Order", new { id = Models.Assets.ShopInfo.StoreId });
                }
            }

            //pass locations, orders, ordertype
            var stores = _PBrepository.GetAllStores();
            Models.Assets.Stores = stores;
            return View();
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