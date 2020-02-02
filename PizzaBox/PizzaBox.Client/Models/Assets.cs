using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Entities;

namespace PizzaBox.Client.Models
{
    public static class Assets
    {
        //PER ORDER
        public static Store ShopInfo { get; set; }
        public static int NumPresets { get; set; }
        public static int NumCustoms { get; set; }
        public static string Current_user { get; set; }
        public static string Preset_seq { get; set; }
        public static string Custom_seq { get; set; }
        public static int Total_pizzas { get; set; }
        public static decimal Order_cost { get; set; }

        //Session Info
        public static bool Session { get; set; }

        //Locations
        public static IEnumerable<Store> Stores { get; set; }


    }
}
