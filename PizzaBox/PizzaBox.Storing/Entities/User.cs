﻿using System;
using System.Collections.Generic;

namespace PizzaBox.Storing.Entities
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Orders>();
        }

        public string Username { get; set; }
        public string Pass { get; set; }
        public string FullName { get; set; }
        public byte SessionLive { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
