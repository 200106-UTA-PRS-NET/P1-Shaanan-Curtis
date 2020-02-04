using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Client.Models;
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

            //get all locations
            var stores = _PBrepository.GetAllStores();
            Models.Assets.Stores = stores;
            return View();
        }

        public IActionResult History()
        {
            if (Models.Assets.StoreSession == false)
            {
                return Redirect("~/Home/Signin");
            }


            return View();
        }

        public IActionResult ShopHistory()
        {
            if (Models.Assets.StoreSession == false)
            {
                return Redirect("~/Home/Signin");
            }

            //get all locations
            var stores = _PBrepository.GetAllStores();
            Models.Assets.Stores = stores;
            return View();
        }


        public IActionResult Recent(int id)
        {
            if (Models.Assets.StoreSession == false)
            {
                return Redirect("~/Home/Signin");
            }

            Assets.ShopInfo = _PBrepository.GetStoreById(id);
            ShopHistoryModel ShopHistory = new ShopHistoryModel(_PBrepository, id.ToString());
            if (ShopHistory.none)
                ViewData["No_History"] = "No orders have been made at this location.";

            return View(ShopHistory);
        }

        public IActionResult AllOrders()
        {
            if (Models.Assets.StoreSession == false)
            {
                return Redirect("~/Home/Signin");
            }

            ShopHistoryModel AllHistory = new ShopHistoryModel(_PBrepository);
            if (AllHistory.none)
                ViewData["No_History"] = "No orders have been made.";

            return View(AllHistory);
        }

        public IActionResult Inventory()
        {
            if (Models.Assets.StoreSession == false)
            {
                return Redirect("~/Home/Signin");
            }

            //get all locations
            var stores = _PBrepository.GetAllStores();
            Models.Assets.Stores = stores;
            return View();
        }

        public IActionResult Supplies(int id)
        {
            if (Models.Assets.StoreSession == false)
            {
                return Redirect("~/Home/Signin");
            }

            var InventoryInfo = new InventoryModel();
            InventoryInfo.inventory = _PBrepository.GetInventoryByStore(id);
            InventoryInfo.store = _PBrepository.GetStoreById(id);

            return View(InventoryInfo);
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