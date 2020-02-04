using PizzaBox.Storing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBox.Client.Models
{
    public class InventoryModel
    {
        public Inventory inventory { get; set; } = new Inventory();
        public Store store { get; set; } = new Store();
    }
}
