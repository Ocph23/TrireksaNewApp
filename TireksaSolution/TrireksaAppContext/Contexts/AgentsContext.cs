using Microsoft.AspNet.SignalR;
using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TrireksaAppContext
{
    public  class AgentsContext:ITrireksaAppHub
    {

        private readonly static Lazy<AgentsContext> _instance = new Lazy<AgentsContext>(() => new AgentsContext(GlobalHost.ConnectionManager.GetHubContext<TrireksaAppHub>().Clients));
        private Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients;

        public AgentsContext(Microsoft.AspNet.SignalR.Hubs.IHubConnectionContext<dynamic> clients)
        {
            this.clients = clients;
        }

        public static AgentsContext Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public void BroadcastData(agent data)
        {
            clients.All.OnAddAgent(data);
                //clients.AllExcept(conId).onAddAgent(data);
        }


        public IEnumerable<agent> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Agents.Select().ToList();
                foreach (var item in result)
                {
                    item.CitiesCanAccess = db.CitiesAgentCanAccess.Where(O => O.AgentId == item.Id).ToList();
                }
                return result;
            }
        }

        // GET: api/Agents/5

        public agent Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Agents.Where(O => O.Id == id).FirstOrDefault();
            }
        }

        // POST: api/Agents
        public  agent Post(agent value)
        {
            using (var db = new OcphDbContext())
            {
                value.Id = db.Agents.InsertAndGetLastID(value);
                if (value.Id > 0)
                {
                   
                    return  value;
                }
                   
                else
                    
                    throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
            }
        }

        // PUT: api/Agents/5
        public bool Put(int id, agent value)
        {
            using (var db = new OcphDbContext())
            {
                return db.Agents.Update(O => new { O.Address, O.CityID, O.ContactName, O.Email, O.Handphone, O.Name, O.NPWP, O.Phone },
                    value, O => O.Id == value.Id);
            }
        }

        // DELETE: api/Agents/5
        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Agents.Delete(O => O.Id == id);
            }
        }

        public void OnAddAgent(object t)
        {
            throw new NotImplementedException();
        }
    }
}
