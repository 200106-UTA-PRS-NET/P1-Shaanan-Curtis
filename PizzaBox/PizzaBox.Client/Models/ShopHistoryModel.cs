using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBox.Storing.Entities;
using PizzaBox.Storing.Interfaces;

namespace PizzaBox.Client.Models
{
    public class ShopHistoryModel
    {
        private readonly IPizzaBoxRepository _PBrepository;
        public Orders[] O;
        public Ordertype[] OT;
        private long[] ids;

        public string shopid { get; set; }
        public int shop_orders { get; set; }
        public bool none { get; set; }
        
        public ShopHistoryModel(IPizzaBoxRepository PBrepository)
        {
            _PBrepository = PBrepository;
            shop_orders = GetStoreOrders().Count();

            if (shop_orders < 1)
            {
                none = true;
                return;
            }

            O = new Orders[shop_orders];
            OT = new Ordertype[shop_orders];
            ids = new long[shop_orders];

            InitOOTIDS();
            MapOrderResults();
        }

        public ShopHistoryModel(IPizzaBoxRepository PBrepository, string id)
        {
            _PBrepository = PBrepository;
            shop_orders = GetStoreOrders(id).Count();
            shopid = id;
            if(shop_orders < 1)
            {
                none = true;
                return;
            }

            O = new Orders[shop_orders];
            OT = new Ordertype[shop_orders];
            ids = new long[shop_orders];

            InitOOTIDS();
            MapOrderResults();
        }

        private void MapOrderResults()
        {
            int i = 0;
            foreach(var val in GetStoreOrders(shopid))
            {
                O[i] = val;
                ids[i] = val.OrderId;
                i++;
            }

            for(i=0;i<shop_orders; i++)
                OT[i] = _PBrepository.GetOrdertypeById(ids[i]);
        }
        private void InitOOTIDS()
        {
            int i;
            for (i = 0; i < shop_orders; i++)
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
                ids[i] = new long();
            }
        }

        private IEnumerable<Orders> GetStoreOrders()
        {
            return _PBrepository.GetOrdersBy("", "all");
        }
        private IEnumerable<Orders> GetStoreOrders(string id)
        {
            return _PBrepository.GetOrdersBy(id, "store");
        }

    }
}
