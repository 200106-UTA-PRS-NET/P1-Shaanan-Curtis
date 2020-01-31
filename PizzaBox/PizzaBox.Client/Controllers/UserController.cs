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
        public string ValidateSignin(Models.Assets model)
        {
            if(_PBrepository.UserAuthentication(model.user.Username, model.user.Pass) == null)
            {
                return "Username/Password Incorrect";
            }

            return "Signing in";
        }

        public IActionResult Signup()
        {
            return View(UserAssets);
        }

        [HttpPost]
        public string ValidateSignup(Models.Assets model)
        {
            if(_PBrepository.GetUserById(model.user.Username) != null)
            {
                return "User already exists";
            }

            return "Signing you up";
        }
    }
}