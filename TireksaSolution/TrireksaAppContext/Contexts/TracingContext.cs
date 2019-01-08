using ModelsShared;
using ModelsShared.Models;
using Ocph.DAL;
using Ocph.DAL.Mapping;
using Ocph.DAL.Mapping.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrireksaAppContext
{
    public class TracingContext
    {
        public TracingModel GetPenjualan(int STT)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var cmd = db.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "GetPenjualan";
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("STT", STT));
                    var reader = cmd.ExecuteReader();
                    var mapp = new MappingColumn(new EntityInfo(typeof(TracingModel)));
                    var result = mapp.MappingWithoutInclud<TracingModel>(reader);

                    reader.Close();

                    var tracing = result.FirstOrDefault();
                    if (result != null)
                    {
                        var packlists = db.PackingLists.Where(O => O.PenjualanId == tracing.Id).GroupBy(O => O.ManifestID).ToList();
                        tracing.Manifests = new List<manifestoutgoing>();
                        foreach (var item in packlists)
                        {
                            var manifest = db.Outgoing.Where(O => O.Id == item.Key).FirstOrDefault();
                            if (manifest != null)
                                tracing.Manifests.Add(manifest);
                        }
                        return tracing;
                    }
                    else
                    {
                        throw new SystemException(MessageCollection.Message(MessageType.NotFound));
                    }

                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
               
            }

        }


    }
}
