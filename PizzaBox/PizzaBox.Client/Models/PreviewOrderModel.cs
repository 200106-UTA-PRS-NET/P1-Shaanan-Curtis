using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBox.Client.Models
{
    public class PreviewOrderModel
    {
        public int Amount_Pizzas { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }
        public string Style_Pizza { get; set; }
        public decimal Item_Cost { get; set; }

        public void ResetPreview()
        {
            Amount_Pizzas = 0;
            Size = "";
            Crust = "";
            Style_Pizza = "";
            Item_Cost = 0m;
        }
    }
}
