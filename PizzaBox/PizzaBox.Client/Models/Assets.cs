using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Entities;

namespace PizzaBox.Client.Models
{
    public class Assets
    {
        public IEnumerable<Store> Stores;
        public User user = new User()
        {
            Username = "",
            Pass = "",
            SessionLive = 0
        };
    }
}
