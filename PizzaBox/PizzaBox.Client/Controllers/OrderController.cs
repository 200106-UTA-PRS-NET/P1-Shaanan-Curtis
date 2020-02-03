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
            model.Sizes = GetSelectListItems(sizes);

            return View(model);
        }

        public IActionResult Step2(OrderInfoModel model)
        {
            //Assets.Total_pizzas += model.NumCustoms;
            Assets.Total_pizzas += model.NumPresets;
            //Assets.Custom_seq += model.NumCustoms.ToString();
            Assets.Preset_seq += model.NumPresets.ToString();

            //ADD TO TOTAL COST FOR THIS ORDER
            //For Style of Pizza
            switch(model.Pizza)
            {
                case "Vegan":
                    switch(model.Size)
                    {
                        case "Small":
                            Assets.Order_cost += (4.00m * model.NumPresets);
                            break;
                        case "Medium":
                            Assets.Order_cost += (8.00m * model.NumPresets);
                            break;
                        case "Large":
                            Assets.Order_cost += (12.00m * model.NumPresets);
                            break;
                    }
                    break;
                case "Pepperoni":
                    switch(model.Size)
                    {
                        case "Small":
                            Assets.Order_cost += (5.00m * model.NumPresets);
                            break;
                        case "Medium":
                            Assets.Order_cost += (10.00m * model.NumPresets);
                            break;
                        case "Large":
                            Assets.Order_cost += (15.00m * model.NumPresets);
                            break;
                    }
                    break;
                case "BBQ Chicken":
                    switch(model.Size)
                    {
                        case "Small":
                            Assets.Order_cost += (6.00m * model.NumPresets);
                            break;
                        case "Medium":
                            Assets.Order_cost += (12.00m * model.NumPresets);
                            break;
                        case "Large":
                            Assets.Order_cost += (18.00m * model.NumPresets);
                            break;
                    }
                    break;
                case "Meatball":
                    switch(model.Size)
                    {
                        case "Small":
                            Assets.Order_cost += (7.00m * model.NumPresets);
                            break;
                        case "Medium":
                            Assets.Order_cost += (14.00m * model.NumPresets);
                            break;
                        case "Large":
                            Assets.Order_cost += (21.00m * model.NumPresets);
                            break;
                    }
                    break;
                case "Supreme":
                    switch(model.Size)
                    {
                        case "Small":
                            Assets.Order_cost += (8.00m * model.NumPresets);
                            break;
                        case "Medium":
                            Assets.Order_cost += (16.00m * model.NumPresets);
                            break;
                        case "Large":
                            Assets.Order_cost += (24.00m * model.NumPresets);
                            break;
                    }
                    break;
                case "Greek":
                    switch(model.Size)
                    {
                        case "Small":
                            Assets.Order_cost += (9.00m * model.NumPresets);
                            break;
                        case "Medium":
                            Assets.Order_cost += (18.00m * model.NumPresets);
                            break;
                        case "Large":
                            Assets.Order_cost += (27.00m * model.NumPresets);
                            break;
                    }
                    break;
                    /*
                case "Custom":
                    switch(model.Size)
                    {
                        case "Small":
                            Assets.Order_cost += (3 * model.NumCustoms);
                            break;
                        case "Medium":
                            Assets.Order_cost += (6 * model.NumCustoms);
                            break;
                        case "Large":
                            Assets.Order_cost += (9 * model.NumCustoms);
                            break;
                    }
                    break;
                    */
            }

            /*
            //For toppings
            if(model.Toppings.Length > 0)
            {
                foreach(var t in model.Toppings)
                {
                    switch(t)
                    {
                        case "Veggies/Fruit":
                            Assets.Order_cost += 0.50m;
                            break;
                        case "Pepperoni":
                            Assets.Order_cost += 1;
                            break;
                        case "Chicken":
                            Assets.Order_cost += 1;
                            break;
                        case "Meatballs":
                            Assets.Order_cost += 3;
                            break;
                    }
                }
                
            }
            */


            //APPEND TO SEQUENCE FOR THIS ORDER
            switch(model.Size)
            {
                case "Small":
                    /*
                    if (model.NumCustoms > 0)
                        Assets.Custom_seq += 'S';
                    else if (model.NumPresets > 0)
                        Assets.Preset_seq += 'S';
                     */
                    Assets.Preset_seq += 'S';
                    break;
                case "Medium":
                    /*
                    if (model.NumCustoms > 0)
                        Assets.Custom_seq += 'M';
                    else if (model.NumPresets > 0)
                        Assets.Preset_seq += 'M';
                    */
                    Assets.Preset_seq += 'M';
                    break;
                case "Large":
                    /*
                    if (model.NumCustoms > 0)
                        Assets.Custom_seq += 'L';
                    else if (model.NumPresets > 0)
                        Assets.Preset_seq += 'L';
                    */
                    Assets.Preset_seq += 'L';
                    break;
            }

            switch(model.Crust)
            {
                case "Thick":
                    /*
                    if (model.NumCustoms > 0)
                        Assets.Custom_seq += 'k';
                    else if (model.NumPresets > 0)
                        Assets.Preset_seq += 'k';
                    */
                    Assets.Preset_seq += 'k';
                    break;
                case "Thin":
                    /*
                    if (model.NumCustoms > 0)
                        Assets.Custom_seq += 'n';
                    else if (model.NumPresets > 0)
                        Assets.Preset_seq += 'n';
                    */
                    Assets.Preset_seq += 'n';
                    break;
            }

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
    }
}