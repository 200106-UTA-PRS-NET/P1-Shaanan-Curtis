using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing.Interfaces;

namespace PizzaBox.Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly IPizzaBoxRepository _PBrepository; 
        public OrderController(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
        }
        public IActionResult Step1(int id)
        {
            return View(_PBrepository.GetStoreById(id));
        }
    }
}