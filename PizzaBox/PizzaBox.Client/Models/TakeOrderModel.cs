using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBox.Client.Models
{
    public class TakeOrderModel
    {
        // This property will hold number of preset pizzas, selected by user
        [Required]
        public short NumPresets { get; set; }

        //This property will hold the selected crust
        [Required]
        [Display(Name = "Crust")]
        public string Crust { get; set; }

        //This property will hold the choice of pizza
        [Required]
        public string Pizza { get; set; }

        // This property will hold the selected size
        public string Size { get; set; }

        // This property will hold all available sizes for dropdown selection
        public IEnumerable<SelectListItem> Sizes { get; set; }

        public void ResetOrder()
        {
            NumPresets = 0;
            Crust = "";
            Pizza = "";
            Size = "";
        }
        ///This property will hold number of custom pizzas, selected by user
        ///[Required]
        ///public short NumCustoms { get; set; }

        ///This property will hold the selected toppings
        ///public string[] Toppings { get; set; }
    }
}
