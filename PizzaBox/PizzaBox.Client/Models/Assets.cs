using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Entities;

namespace PizzaBox.Client.Models
{
    public static class Assets
    {
        public static int Storeid { get; set; }
        public static bool Session { get; set; }
        public static string Current_user { get; set; }
        public static IEnumerable<Store> Stores { get; set; }
    }
}
