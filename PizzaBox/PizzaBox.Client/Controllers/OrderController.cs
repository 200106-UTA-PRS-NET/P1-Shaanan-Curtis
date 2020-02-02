using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaBox.Storing.Interfaces;
using PizzaBox.Client.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PizzaBox.Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly IPizzaBoxRepository _PBrepository;
        private readonly OrderInfoModel order_information = new OrderInfoModel();

        public OrderController(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
        }
        public IActionResult Step1(int id)
        {
            Models.Assets.ShopInfo = _PBrepository.GetStoreById(id);

            var sizes = GetAllSizes();
            var model = new OrderInfoModel();
            model.StoreId = id;
            model.Sizes = GetSelectListItems(sizes);

            return View(model);
        }

        // return a list of available sizes
        private IEnumerable<string> GetAllSizes()
        {
            return new List<string>
            {
                "Small",
                "Medium",
                "Large",
            };
        }

        // Takes a list of strings and returns a list of SelectListItem objects.
        // renders the DropDownList
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Empty list for holding result
            var selectList = new List<SelectListItem>();

            // For each string in elements, create a new SelectListItem object
            // <option value="Size Name">Size Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        public IActionResult Step2(OrderInfoModel model)
        {
            
            return View();
        }
    }
}