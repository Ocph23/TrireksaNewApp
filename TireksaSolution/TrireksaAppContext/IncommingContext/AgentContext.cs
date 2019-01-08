using Incomming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaAppContext.IncommingContext
{
    public  class AgentContext
    {

        public Task<List<agents>> Get()
        {
            using (var db = new InccomingDbContext()) 
            {
                var result = db.Agents.Select().ToList();
                return Task.FromResult(result);
            }
        }

        public Task<agents> GetById(int id)
        {
            using (var db = new InccomingDbContext())
            {
                var result = db.Agents.Where(O => O.Id == id).FirstOrDefault();
                return Task.FromResult(result);
            }
        }

        public Task<agents> Post(agents agent)
        {
            using (var db = new InccomingDbContext())
            {
                agent.Id = db.Agents.InsertAndGetLastID(agent);
                if (agent.Id > 0)
                    return Task.FromResult(agent);
                else
                    throw new SystemException("Data Tidak Terhapus");
            }
        }


        public Task<agents> Put(agents agent)
        {
            using (var db = new InccomingDbContext())
            {
                var isUpdated = db.Agents.Update(O=> new {O.Address, O.ContactPerson, O.Name,O.Telepon },agent,O=>O.Id==agent.Id);
                if (isUpdated)
                    return Task.FromResult(agent);
                else
                    throw new SystemException("Data Tidak Berhasil Diubah");
            }
        }


    }
}
