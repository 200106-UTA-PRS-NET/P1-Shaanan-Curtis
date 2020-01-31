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
        private readonly Models.Assets UserAssets = new Models.Assets();
        public UserController(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
        }

        public IActionResult Signin()
        {
            return View(UserAssets);
        }

        [HttpPost]
        public IActionResult ValidateSignin(Models.Assets model)
        {
            if(_PBrepository.UserAuthentication(model.user.Username, model.user.Pass) == null)
            {
                ViewBag.Auth_Failure = true;
                return View("Signin");
            }
            ViewBag.Auth_Failure = false;
            ViewBag.Session = true;
            model.user.SessionLive = 1;
            _PBrepository.UpdateUser(model.user);
            return Redirect("/Home/Index");
        }

        public IActionResult Signup()
        {
            return View(UserAssets);
        }

        [HttpPost]
        public IActionResult ValidateSignup(Models.Assets model)
        {
            if(_PBrepository.GetUserById(model.user.Username) != null)
            {
                ViewBag.User_Exists = true;
                return View("Signup");
            }

            ViewBag.User_Exists = false;
            model.user.SessionLive = 1;
            _PBrepository.AddUser(model.user);
            ViewBag.Session = true;
            return Redirect("/Home/Index");
        }

        public IActionResult Signout()
        {
            //make a way to pass in user and set sessionlive = 0
            ViewBag.Session = false;
            return Redirect("/Home/Index");
        }
    }
}