using Incomming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaAppContext.IncommingContext
{
    public class ManifestContext
    {
        public Task<List<manifestsincomming>> Get()
        {
            using (var db = new InccomingDbContext())
            {
                var result = db.Manifests.Select().ToList();
                return Task.FromResult(result);
            }
        }

        public Task<manifestsincomming> GetById(int id)
        {
            using (var db = new InccomingDbContext())
            {
                var result = db.Manifests.Where(O => O.Id == id).FirstOrDefault();
                return Task.FromResult(result);
            }
        }

        public Task<manifestsincomming> Post(manifestsincomming item)
        {
            using (var db = new InccomingDbContext())
            {
                item.Id = db.Manifests.InsertAndGetLastID(item);
                if (item.Id > 0)
                    return Task.FromResult(item);
                else
                    throw new SystemException("Data Tidak Terhapus");
            }
        }


        public Task<manifestsincomming> Put(manifestsincomming item)
        {
            using (var db = new InccomingDbContext())
            {
                var isUpdated = db.Manifests.Update(O => new { O.ManifestNumber,O.UpdateDate,O.UserId,O.Via }, item, O => O.Id == item.Id);
                if (isUpdated)
                    return Task.FromResult(item);
                else
                    throw new SystemException("Data Tidak Berhasil Diubah");
            }
        }
    }
}