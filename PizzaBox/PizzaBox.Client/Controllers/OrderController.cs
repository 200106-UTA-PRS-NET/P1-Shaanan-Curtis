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
        private bool computed;
        public OrderController(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
        }

        public IActionResult Step1(int id)
        {
            if (Models.Assets.Session == false)
            {
                return Redirect("~/Home/Signin");
            }
        
            Assets.ShopInfo = _PBrepository.GetStoreById(id);
            var sizes = GetAllSizes();
            var model = new TakeOrderModel();
            model.Sizes = GetSelectListItems(sizes);

            return View(model);
        }

        public IActionResult Step2(TakeOrderModel model)
        {
            if (Models.Assets.Session == false)
            {
                return Redirect("~/Home/Signin");
            }
            if (!computed)
            {
                computed = true;

                Assets.Total_pizzas += model.NumPresets;
                Assets.NumPresets += model.NumPresets;
                //Assets.Total_pizzas += model.NumCustoms;
                //Assets.Custom_seq += model.NumCustoms.ToString();

                //ADD TO TOTAL COST FOR THIS ORDER
                decimal icost = 0.00m;
                ///for pizza choice
                switch (model.Pizza)
                {
                    case "Vegan":
                        switch (model.Size)
                        {
                            case "Small":
                                Assets.Subtotal += (4.00m * model.NumPresets);
                                icost += (4.00m * model.NumPresets);
                                break;
                            case "Medium":
                                Assets.Subtotal += (8.00m * model.NumPresets);
                                icost += (8.00m * model.NumPresets);
                                break;
                            case "Large":
                                Assets.Subtotal += (12.00m * model.NumPresets);
                                icost += (12.00m * model.NumPresets);
                                break;
                        }
                        break;
                    case "Pepperoni":
                        switch (model.Size)
                        {
                            case "Small":
                                Assets.Subtotal += (5.00m * model.NumPresets);
                                icost += (5.00m * model.NumPresets);
                                break;
                            case "Medium":
                                Assets.Subtotal += (10.00m * model.NumPresets);
                                icost += (10.00m * model.NumPresets);
                                break;
                            case "Large":
                                Assets.Subtotal += (15.00m * model.NumPresets);
                                icost += (15.00m * model.NumPresets);
                                break;
                        }
                        break;
                    case "BBQ Chicken":
                        switch (model.Size)
                        {
                            case "Small":
                                Assets.Subtotal += (6.00m * model.NumPresets);
                                icost += (6.00m * model.NumPresets);
                                break;
                            case "Medium":
                                Assets.Subtotal += (12.00m * model.NumPresets);
                                icost += (12.00m * model.NumPresets);
                                break;
                            case "Large":
                                Assets.Subtotal += (18.00m * model.NumPresets);
                                icost += (18.00m * model.NumPresets);
                                break;
                        }
                        break;
                    case "Meatball":
                        switch (model.Size)
                        {
                            case "Small":
                                Assets.Subtotal += (7.00m * model.NumPresets);
                                icost += (7.00m * model.NumPresets);
                                break;
                            case "Medium":
                                Assets.Subtotal += (14.00m * model.NumPresets);
                                icost += (14.00m * model.NumPresets);
                                break;
                            case "Large":
                                Assets.Subtotal += (21.00m * model.NumPresets);
                                icost += (21.00m * model.NumPresets);
                                break;
                        }
                        break;
                    case "Supreme":
                        switch (model.Size)
                        {
                            case "Small":
                                Assets.Subtotal += (8.00m * model.NumPresets);
                                icost += (8.00m * model.NumPresets);
                                break;
                            case "Medium":
                                Assets.Subtotal += (16.00m * model.NumPresets);
                                icost += (16.00m * model.NumPresets);
                                break;
                            case "Large":
                                Assets.Subtotal += (24.00m * model.NumPresets);
                                icost += (24.00m * model.NumPresets);
                                break;
                        }
                        break;
                    case "Greek":
                        switch (model.Size)
                        {
                            case "Small":
                                Assets.Subtotal += (9.00m * model.NumPresets);
                                icost += (9.00m * model.NumPresets);
                                break;
                            case "Medium":
                                Assets.Subtotal += (18.00m * model.NumPresets);
                                icost += (18.00m * model.NumPresets);
                                break;
                            case "Large":
                                Assets.Subtotal += (27.00m * model.NumPresets);
                                icost += (27.00m * model.NumPresets);
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

                //VALIDATE ORDER BEFORE ADDING TO SEQUENCE
                PreviewOrderModel OrderItem = new PreviewOrderModel();
                OrderItem.Amount_Pizzas = model.NumPresets;
                OrderItem.Size = model.Size;
                OrderItem.Crust = model.Crust;
                OrderItem.Style_Pizza = model.Pizza;
                OrderItem.Item_Cost = icost;
                Assets.Tax = Assets.Subtotal * 0.08m;
                Assets.Order_Total = Assets.Subtotal + Assets.Tax;

                if (Assets.Order_Total > 250.00m || Assets.Total_pizzas > 100)
                {
                    if (Assets.Order_Total > 250.00m && Assets.Total_pizzas > 100)
                    {
                        ViewData["Limit_Exceeded_Message"] = "Our policy only allows us to complete orders up to $250 and up to 100 pizzas at a time.";
                    }
                    else if (Assets.Order_Total > 250.00m)
                    {
                        ViewData["Limit_Exceeded_Message"] = "Our policy only allows us to complete orders up to $250 at a time.";
                    }
                    else if (Assets.Total_pizzas > 100)
                    {
                        ViewData["Limit_Exceeeded_Message"] = "Our policy only allows us to complete orders up to 100 pizzas at a time.";
                    }

                    Assets.Subtotal -= icost;
                    Assets.Total_pizzas -= OrderItem.Amount_Pizzas;
                    Assets.Tax = Assets.Subtotal * 0.08m;
                    Assets.Order_Total = Assets.Subtotal + Assets.Tax;
                    return View("Message");
                }

                //APPEND TO SEQUENCE FOR THIS ORDER
                Assets.Preset_seq += OrderItem.Amount_Pizzas.ToString();
                switch (model.Size)
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

                switch (model.Crust)
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

                Assets.Q.Add(OrderItem);
                computed = false;
            }
            
            return View(Assets.Q);
        }

        public IActionResult Step3()
        {
            if (Models.Assets.Session == false)
            {
                return Redirect("~/Home/Signin");
            }
            return View(Assets.Q);
        }

        public IActionResult Confirmation()
        {
            if (Models.Assets.Session == false)
            {
                return Redirect("~/Home/Signin");
            }
            Assets.OrderInfo.StoreId = Assets.ShopInfo.StoreId;
            Assets.OrderInfo.Username = Assets.Current_user;
            _PBrepository.AddOrder(Assets.OrderInfo, Assets.OrdertypeInfo, Assets.Preset_seq, "-", decimal.Round(Assets.Order_Total,2));
            _PBrepository.UpdateInventory(Assets.OrderInfo.StoreId, Assets.NumPresets, Assets.NumCustoms, "subtract");
   
            Assets.Ordered_Once = true;

            //reset timer per completed order (1 location/24 hour period)
            Assets.today = new System.DateTime(System.DateTime.Today.Ticks);
            Assets.tomorrow = new System.DateTime(System.DateTime.Today.AddDays(1).Ticks);
            return View();
        }

        public IActionResult Cancel()
        {
            if (Models.Assets.Session == false)
            {
                return Redirect("~/Home/Signin");
            }
            Assets.Q.Clear();
            Assets.ClearOrder();
            
            return View();
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