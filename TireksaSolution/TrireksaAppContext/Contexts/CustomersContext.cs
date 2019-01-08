using Microsoft.AspNet.SignalR;
using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrireksaAppContext
{


    public class CustomersContext
    {

        //SignalR
        private readonly static Lazy<CustomersContext> _instance = new Lazy<CustomersContext>(() => new CustomersContext(GlobalHost.ConnectionManager.GetHubContext<TrireksaAppHub>().Clients));
        private Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;

        public CustomersContext(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients)
        {
            this.clients = clients;
        }
     

        public static CustomersContext Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        public void BroadcastData(customer data)
        {
            clients.All.onAddCustomer(data);
        }


        //End SignalEr

        // GET: api/customers/5




        public IEnumerable<customer> Get()
        {
            using (var db = new OcphDbContext())
            {
                var res = from a in db.Customers.Select()
                          join b in db.Cities.Select() on a.CityID equals b.Id
                          select new customer
                          {
                              Address = a.Address,
                              CityID = a.CityID,
                              ContactName = a.ContactName,
                              CustomerType = a.CustomerType,
                              Id = a.Id,
                              Email = a.Email,
                              Handphone = a.Handphone,
                              Name = a.Name,
                              Phone1 = a.Phone1,
                              Phone2 = a.Phone2,
                              City = b
                          };
                return res.ToList();
            }
        }
        public customer Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var res = from a in db.Customers.Where(O=>O.Id==id)
                          join b in db.Cities.Select() on a.CityID equals b.Id
                          select new customer
                          {
                              Address = a.Address,
                              CityID = a.CityID,
                              ContactName = a.ContactName,
                              CustomerType = a.CustomerType,
                              Id = a.Id,
                              Email = a.Email,
                              Handphone = a.Handphone,
                              Name = a.Name,
                              Phone1 = a.Phone1,
                              Phone2 = a.Phone2,
                              City = b
                          };
                return res.FirstOrDefault();
            }
        }

        // POST: api/customers
        public customer Post(customer value)
        {
            using (var db = new OcphDbContext())
            {
                value.Id= db.Customers.InsertAndGetLastID(value);
                if (value.Id <= 0)
                    throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                else
                {
                    if (value.City == null)
                        value.City = db.Cities.Where(O => O.Id == value.CityID).FirstOrDefault();
                    return value;
                }
                  
            }
        }

        // PUT: api/customers/5

        public customer Put(int id, customer value)
        {
            using (var db = new OcphDbContext())
            {
                if (db.Customers.Update(O => new
                {
                    O.Address,
                    O.ContactName,
                    O.CustomerType,
                    O.Email,
                    O.Handphone,
                    O.Name,
                    O.Phone1,
                    O.Phone2,
                    O.CityID
                }, value, O => O.Id == id))
                {
                    return value;
                }
                else
                    return default(customer);
            }
        }

        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Customers.Delete(O => O.Id == id);
            }
        }
    }
}
