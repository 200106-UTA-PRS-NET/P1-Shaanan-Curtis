using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PizzaBox.Client.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Step1(int id)
        {
            return View(id);
        }
    }
}