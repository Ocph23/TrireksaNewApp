using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrireksaAppContext
{
   public class CitiesAgentCanAccessContext
    {
        // GET: api/CitiesAgentCanAccess/5
        public IEnumerable<CityAgentCanAccess> Get(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.CitiesAgentCanAccess.Where(O => O.AgentId == id).ToList();
            }
        }

        // POST: api/CitiesAgentCanAccess
        public int Post(CityAgentCanAccess value)
        {
            using (var db = new OcphDbContext())
            {
                return db.CitiesAgentCanAccess.InsertAndGetLastID(value);
            }
        }


        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.CitiesAgentCanAccess.Delete(O => O.Id == id);
            }
        }

        public CityAgentCanAccess OnChangeItemTrue(CityAgentCanAccess obj)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    obj.Id = db.CitiesAgentCanAccess.InsertAndGetLastID(obj);
                    if (obj.Id > 0)
                        return obj;
                    else
                        throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);
                }

            }

        }

        public CityAgentCanAccess OnChangeItemFalse(CityAgentCanAccess obj)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.CitiesAgentCanAccess.Delete(O => O.AgentId == obj.AgentId && O.CityId == obj.CityId);
                    if (result)
                        return obj;
                    else
                        return null;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }

            }

        }

    }
}

