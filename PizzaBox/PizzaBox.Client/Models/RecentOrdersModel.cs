using PizzaBox.Storing.Entities;
using PizzaBox.Storing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaBox.Client.Models
{
    public class RecentOrdersModel
    {
        private readonly IPizzaBoxRepository _PBrepository;
        public Orders[] O;
        public Ordertype[] OT;
        private long[] ids;
        public int my_orders;
        public bool none;
        public RecentOrdersModel(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
            my_orders = GetAllOrders().Count();

            if (my_orders < 1)
            {
                none = true;
                return;
            }
      
            O = new Orders[my_orders];
            OT = new Ordertype[my_orders];
            ids = new long[my_orders];
            InitOOTIDS();
            MapOrderResults();
        }
        
        private void MapOrderResults()
        {
            //ORDERS
            int i = 0;
            foreach(var val in GetAllOrders())
            {
                O[i] = val;
                ids[i] = val.OrderId;
                i++;
            }

            //ORDERTYPE
            for (i = 0; i < my_orders; i++)
                OT[i] = _PBrepository.GetOrdertypeById(ids[i]);
        }
        private void InitOOTIDS()
        {
            int i;
            for(i=0;i<my_orders;i++)
            {
                O[i] = new Orders()
                {
                    OrderId = 0,
                    StoreId = 0,
                    Username = ""
                };
                OT[i] = new Ordertype()
                {
                    OrderId = 0,
                    Preset = "",
                    Custom = "",
                    Dt = "",
                    Tm = ""
                };
                ids[i] = new int();
            }
        }
        private IEnumerable<Orders> GetAllOrders()
        {
            return _PBrepository.GetOrdersBy(Assets.Current_user, "user");
        }
    }
}
