using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaBox.Client.Models;
using PizzaBox.Storing.Entities;
using PizzaBox.Storing.Interfaces;

namespace PizzaBox.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaBoxRepository _PBrepository;
        public readonly User HCuser = new User();
        private readonly ILogger<HomeController> _logger;

        public HomeController(IPizzaBoxRepository PBrepository, ILogger<HomeController> logger)
        {
            _PBrepository = PBrepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Signin()
        {
            return View(HCuser);
        }

        [HttpPost]
        public IActionResult Signin(User model)
        {
            if (_PBrepository.UserAuthentication(model.Username, model.Pass) == null)
            {
                //username/password incorrect
                ViewBag.Auth_Error = true;
                return View("Signin");
            }

            //passed authentication
            ViewBag.Auth_Error = false;
            var u = _PBrepository.GetUserById(model.Username);
            u.SessionLive = 1;
            _PBrepository.UpdateUser(u);

            Assets.Session = true;
            return View("Index");
        }

        public IActionResult Signup()
        {
            return View(HCuser);
        }

        [HttpPost]
        public IActionResult Signup(User model)
        {
            var u = _PBrepository.GetUserById(model.Username);
            if (u != null)
            {
                ViewBag.User_Exists = true;
                return View("Signup");
            }

            ViewBag.User_Exists = false;
            model.SessionLive = 1;
            _PBrepository.AddUser(model);

            Assets.Session = true;
            return View("Index");
        }

        public IActionResult Signout()
        {
            //no way of telling who the user is, too many restrictions (user.SessionLive = 0):
            ///can only input at most one model in View()
            //can't persist data between views (probably only one way to do it, which is currently equivalent to a secret menu to me)
            //can't deploy on azure using Identity
            //using static session for now (signin rendered pointless)

            Assets.Session = false;
            return View("Index");
        }
    }
}
