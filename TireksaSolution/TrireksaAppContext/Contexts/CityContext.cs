using Microsoft.AspNet.SignalR;
using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Infrastructure;

namespace TrireksaAppContext
{
 public   class CityContext
    {

        //SignalR
        private readonly static Lazy<CityContext> _instance = new Lazy<CityContext>(() => new CityContext(GlobalHost.ConnectionManager.GetHubContext<TrireksaAppHub>().Clients));
        private Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;

        public CityContext(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients)
        {
            this.clients = clients;
       
        }


        public static CityContext Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        public void BroadcastData(city data)
        {
            clients.All.onAddCity(data);
        }

        //End SignalEr

        public IEnumerable<city> Get()
        {
            using (var db = new OcphDbContext())
            {
                return db.Cities.Select();
            }

        }

        // GET: api/City/5
        public city Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Cities.Where(O => O.Id == id).FirstOrDefault();
            }
        }

        // POST: api/City
        public city Post(city value)
        {
            using (var db = new OcphDbContext())
            {
                value.Id= db.Cities.InsertAndGetLastID(value);
                if (value.Id <= 0)
                    throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                else
                {
                   
                    return value;
                }
                 
            }

        }

        // PUT: api/City/5

        public city Put(int id, city value)
        {
            using (var db = new OcphDbContext())
            {
               if( db.Cities.Update(O => new { O.CityCode, O.CityName, O.Province, O.Regency }, value, O => O.Id == value.Id))
                {
                    return value;
                }else
                {
                    return default(city);
                }
            }
        }

        // DELETE: api/City/5
        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Cities.Delete(O => O.Id == id);
            }
        }
    }
}
