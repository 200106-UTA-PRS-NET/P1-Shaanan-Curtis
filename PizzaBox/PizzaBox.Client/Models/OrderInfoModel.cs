using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBox.Client.Models
{
    public class OrderInfoModel
    {
        public int StoreId { get; set; }

        // This property will hold number of preset pizzas, selected by user
        [Required]
        public int NumPresets { get; set; }

        // This property will hold number of custom pizzas, selected by user
        //[Required]
        //public int NumCustoms { get; set; }
        
        // This property will hold the selected size
        public string Size { get; set; }

        // This property will hold all available sizes for selection
        public IEnumerable<SelectListItem> Sizes { get; set; }

        [Required]
        [Display(Name ="Crust")]
        public string Crust { get; set; }

        [Required]
        [Display(Name ="Specialty Pizzas")]
        public string Pizza { get; set; }

        //public string[] Toppings { get; set; }
    }
}
