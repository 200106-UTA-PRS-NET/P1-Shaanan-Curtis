using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Entities;

namespace PizzaBox.Client.Models
{
    public static class Assets
    {
        // STORE ORDER MODEL
        public static Orders OrderInfo { get; set; } = new Orders();
        public static Ordertype OrdertypeInfo { get; set; } = new Ordertype();
        public static Store ShopInfo { get; set; } = new Store();
        public static string Current_user { get; set; }
        public static string Preset_seq { get; set; } = "";
        //public static string Custom_seq { get; set; } = "";
        public static short NumPresets { get; set; }
        public static short NumCustoms { get; set; }
        public static int Total_pizzas { get; set; }
        public static decimal Subtotal { get; set; }
        public static decimal Tax { get; set; }
        public static decimal Order_Total { get; set; }
        public static List<PreviewOrderModel> Q { get; set;} = new List<PreviewOrderModel>();

        public static void ClearOrder()
        {
            OrderInfo = null;
            OrdertypeInfo = null;
            ShopInfo = null;
            Preset_seq = "";
            NumPresets = 0;
            NumCustoms = 0;
            Total_pizzas = 0;
            Subtotal = 0m;
            Tax = 0m;
            Order_Total = 0m;
        }

        //USER SESSION MODEL
        public static bool Session { get; set; }
        public static bool Ordered_Once { get; set; }
        public static System.DateTime today { get; set; } = DateTime.MinValue;
        public static System.DateTime tomorrow { get; set; } = DateTime.MinValue;

        //LOCATIONS MODEL
        public static IEnumerable<Store> Stores { get; set; }


    }
}
