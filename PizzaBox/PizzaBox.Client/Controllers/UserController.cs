using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing.Interfaces;

namespace PizzaBox.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IPizzaBoxRepository _PBrepository;
        public UserController(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
        }

        public IActionResult Signin()
        {
            //pass user
            return View();
        }

        public IActionResult Signup()
        {
            //pass user
            return View();
        }
    }
}