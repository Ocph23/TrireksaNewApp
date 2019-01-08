using ModelsShared;
using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrireksaAppContext
{
    public class ShipsContext
    {
        // GET: api/Ships
        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Ships.Delete(O => O.Id == id);
            }
        }

        public IEnumerable<ship> Get()
        {
            using (var db = new OcphDbContext())
            {
                return db.Ships.Select().ToList();
            }
        }


        public ship GetById(int Id)
        {
            using (var db = new OcphDbContext())
            {
                return db.Ships.Where(o => o.Id == Id).FirstOrDefault();
            }
        }

        public ship InsertAndGetItem(ship t)
        {
            using (var db = new OcphDbContext())
            {
                t.Id = db.Ships.InsertAndGetLastID(t);
                if (t.Id > 0)
                    return t;
                else
                    throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
            }
        }
        
        public ship UpdateAndGetItem(ship t)
        {
            using (var db = new OcphDbContext())
            {
                if (db.Ships.Update(O => new { O.Description, O.Name }, t, O => O.Id == t.Id))
                {
                    return t;
                }
                else
                    throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
            }
        }
    }
}
