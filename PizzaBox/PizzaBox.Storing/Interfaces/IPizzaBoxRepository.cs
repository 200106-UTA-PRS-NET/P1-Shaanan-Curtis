using System;
using System.Text;
using System.Collections.Generic;
using PizzaBox.Storing.Entities;
using System.Linq;

namespace PizzaBox.Storing.Interfaces
{
    public interface IPizzaBoxRepository : IDisposable
    {
        //useful for discovering empty inventories
        IEnumerable<Inventory> GetInventoryByType(string type = "preset", int search = 0);

        Inventory GetInventoryByStore(int storeid);

        int AddInventory(Inventory inventory);

        void UpdateInventory(int id, short preset, short custom, string type);

        IEnumerable<Orders> GetOrdersBy(string search, string type = "user");

        IEnumerable<Ordertype> GetAllOrdertypes();

        Ordertype GetOrdertypeById(long id);

        void AddOrder(Orders orders, Ordertype ordertype, string preset, string custom, decimal cost);

        IEnumerable<Store> GetAllStores();

        Store GetStoreById(int id);

        void AddStore(Store store);

        IQueryable GetUsersByStoreId(int id);

        User GetUserById(string uname);

        User UserAuthentication(string uname, string pass);

        void AddUser(User user);

        void UpdateUser(User user);

        void save();
    }
}
