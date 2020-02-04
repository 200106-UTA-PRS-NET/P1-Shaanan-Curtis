using System;
using System.Text;
using System.Collections.Generic;
using PizzaBox.Storing.Interfaces;
using PizzaBox.Storing.Entities;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace PizzaBox.Storing.Repositories
{
    public class PizzaBoxRepository : IPizzaBoxRepository, IDisposable
    {
        private readonly PIZZABOXContext _dbContext;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PizzaBoxRepository(PIZZABOXContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Inventory> GetInventoryByType(string type = "preset", int search = 0)
        {
            IQueryable<Inventory> results = _dbContext.Inventory.Include(s => s.Store).AsNoTracking();

            if (search > -1)
            {
                switch (type.ToLower())
                {
                    case "preset":
                        results = results.Where(i => i.Preset == search);
                        break;
                    case "custom":
                        results = results.Where(i => i.Custom == search);
                        break;
                }
            }

            return results;
        }

        public Inventory GetInventoryByStore(int storeid)
        {
            return _dbContext.Inventory.Find(storeid);
        }

        public int AddInventory(Inventory inventory)
        {
            if (inventory.StoreId == 0)
            {
                //logger.Error($"Inventory record has an invalid ID of ({inventory.StoreId}): Cannot add to DB.");
                return 1;
            }

            //logger.Info($"Adding inventory");

            _dbContext.Add(inventory);
            return 0;
        }

        public void UpdateInventory(int id, short preset, short custom, string type)
        {
            //logger.Info($"Updating inventory with ID {id}");
            var ret = from i in _dbContext.Inventory where i.StoreId == id select i;

            if (ret.Count() < 1)
            {
                //logger.Warn($"Inventory with ID ({id}) does not exist.");
                return;
            }
            switch (type.ToLower())
            {
                case "add":
                    ret.SingleOrDefault().Preset += preset;
                    ret.SingleOrDefault().Custom += custom;
                    break;

                case "subtract":
                    if (ret.SingleOrDefault().Preset < preset)
                    {
                        //logger.Warn($"Only {ret.SingleOrDefault().Preset} presets available: Cannot complete order.");
                        return;
                    }
                    else if (ret.SingleOrDefault().Custom < custom)
                    {
                        //logger.Warn($"Only {ret.SingleOrDefault().Custom} customs available: Cannot complete order.");
                        return;
                    }

                    ret.SingleOrDefault().Preset -= preset;
                    ret.SingleOrDefault().Custom -= custom;
                    break;
            }

            save();
        }

        public IEnumerable<Orders> GetOrdersBy(string search, string type = "user")
        {
            IQueryable<Orders> orders;
            if (type.ToLower() == "all")
            {
                orders = _dbContext.Orders;
                return orders;
            }
            else if (type.ToLower() == "store")
            {
                int id = 0;
                if (!int.TryParse(search, out id))
                {
                    ///logger.Warn($"ID ({search}) could not be read.");
                    return null;
                }
                else
                {
                    if (id < 1)
                    {
                        ///logger.Warn($"Invalid ID ({id}).");
                        return null;
                    }
                }
                orders = _dbContext.Orders.Where(o => o.StoreId == id);
                return orders;
            }
            else if (type.ToLower() != "user")
                type = "user";

            orders = _dbContext.Orders.Where(o => o.Username == search);
            return orders;
        }

        public IEnumerable<Ordertype> GetAllOrdertypes()
        {
            var result = from ot in _dbContext.Ordertype select ot;
            return result;
        }

        public Ordertype GetOrdertypeById(int id)
        {
            IQueryable<Ordertype> query = from ot in _dbContext.Ordertype where ot.OrderId == id select ot;
            return query.SingleOrDefault();
        }

        public void AddOrder(Orders orders, Ordertype ordertype, string preset, string custom, decimal cost)
        {
            _dbContext.Add(orders);
            save();

            var inner = from ot in _dbContext.Ordertype select ot.OrderId;
            var outer = from o in _dbContext.Orders where !inner.Contains(o.OrderId) select o;
            ordertype.OrderId = outer.FirstOrDefault().OrderId;
            ordertype.Preset = preset;
            ordertype.Custom = custom;
            DateTime date = DateTime.Now;
            ordertype.Dt = date.ToString("MM/dd/yyyy");
            ordertype.Tm = date.ToString("HH:mm");
            ordertype.Cost = cost;
            _dbContext.Add(ordertype);
            save();
        }

        public IEnumerable<Store> GetAllStores()
        {
            var query = from s in _dbContext.Store select s;
            return query;
        }

        public Store GetStoreById(int id)
        {
            var query = from s in _dbContext.Store where s.StoreId == id select s;

            return query.SingleOrDefault();
        }

        public void AddStore(Store store)
        {
            _dbContext.Add(store);
            save();

            Inventory newInventory = new Inventory();
            newInventory.StoreId = store.StoreId;
            newInventory.Preset = 1000;
            newInventory.Custom = 1000;
            AddInventory(newInventory);
        }

        public IQueryable GetUsersByStoreId(int id)
        {
            IQueryable<Orders> results = _dbContext.Orders.Where(o => o.StoreId == id);
            return results.Select(o => o.Username);
        }

        public User GetUserById(string uname)
        {
            return _dbContext.User.Find(uname);
        }

        public User UserAuthentication(string uname, string pass)
        {
            if (_dbContext.User.Where(u => u.Username.Contains(uname) && u.Pass.Contains(pass)).Count() < 1)
            {
                return null;
            }

            var query = from u in _dbContext.User where u.Username == uname && u.Pass == pass select u;

            return query.SingleOrDefault();
        }

        public void AddUser(User user)
        {
            _dbContext.Add(user);
            save();
        }

        public void UpdateUser(User user)
        {
            var query = from u in _dbContext.User where u.Username == user.Username select u;
            //query.SingleOrDefault().FullName = user.FullName;
            query.SingleOrDefault().SessionLive = user.SessionLive;
            save();
        }

        public bool IsSignedIn(string username)
        {
            var query = _dbContext.User.Where(u => u.Username == username);

            if(query.SingleOrDefault().SessionLive == 1)
                return true;

            return false;
        }
        public void save()
        {
            //logger.Info("Saving changes to database");
            _dbContext.SaveChanges();
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
