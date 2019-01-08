using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ModelsShared.Models;
using ModelsShared;
using Microsoft.AspNet.SignalR;

namespace TrireksaAppContext
{



   public class PortsContext
    {

        private readonly static Lazy<PortsContext> _instance = new Lazy<PortsContext>(() => new PortsContext(GlobalHost.ConnectionManager.GetHubContext<TrireksaAppHub>().Clients));
        private Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;

        public PortsContext(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients)
        {
            this.clients = clients;
        }

        public static PortsContext Instance
        {
            get
            {
                return _instance.Value;
            }
        }


        public void BroadcastData(port data)
        {
            clients.All.onAddPort(data);
        }


        public IEnumerable<ModelsShared.Models.port> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = from a in db.Ports.Select()
                             join b in db.Cities.Select(O => new { O.Id, O.CityName }) on a.CityID equals b.Id
                             select new ModelsShared.Models.port
                             {
                                 CityID = a.CityID,
                                 CityName = b.CityName,
                                 Code = a.Code,
                                 Name = a.Name,
                                 Id = a.Id,
                                 PortType = a.PortType
                             };
                return result;

            }
        }

        public port Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Ports.Where(O => O.Id == id).FirstOrDefault();
                result.CityName = db.Cities.Where(O => O.Id == result.CityID).FirstOrDefault().CityName;
                return result;
            }
        }

        public port Post(port value)
        {
            using (var db = new OcphDbContext())
            {
                value.Id= db.Ports.InsertAndGetLastID(value);
                if (value.Id > 0)
                {
                 
                    return value;
                }
                   
                else
                    throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
            }
        }

        public bool Put(int id, port value)
        {
            using (var db = new OcphDbContext())
            {
                return db.Ports.Update(O => new { O.CityID, O.Code, O.Name, O.PortType }, value, O => O.Id == id);
            }
        }

        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Ports.Delete(O => O.Id == id);
            }
        }
    }
}
