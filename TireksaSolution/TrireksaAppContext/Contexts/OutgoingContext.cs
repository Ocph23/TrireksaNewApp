using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using Ocph.DAL;
using Ocph.DAL.Mapping;
using ModelsShared;
using Ocph.DAL.Mapping.MySql;

namespace TrireksaAppContext
{
    public class OutgoingContext
    {
        public IEnumerable<manifestoutgoing> Get()
        {
            using (var db = new OcphDbContext())
            {
                var result = (from m in db.Outgoing.Select()
                              join a in db.Agents.Select() on m.AgentId equals a.Id
                              join o in db.Ports.Select() on m.Origin equals o.Id
                              join d in db.Ports.Select() on m.Destination equals d.Id
                              select new ModelsShared.Models.manifestoutgoing
                              {
                                  AgentId = m.AgentId,
                                  Agent = a,
                                  Code = m.Code,
                                  CreatedDate = m.CreatedDate,
                                  DestinationPort = d,
                                  Destination = m.Destination,
                                  Id = m.Id,
                                  OnDestinationPort = m.OnDestinationPort,
                                  OnOriginPort = m.OnOriginPort,
                                  Origin = m.Origin,
                                  OriginPort = o,
                                  PortType = m.PortType,
                                  UpdateDate = m.UpdateDate,
                                  UserId = m.UserId,
                                  PackingList = db.PackingLists.Where(O => O.ManifestID == m.Id).ToList(),
                                  Information = db.ManifestInformations.Where(O => O.ManifestId == m.Id).FirstOrDefault()


                              });


                return result.ToList();
            }
        }


        public IEnumerable<manifestoutgoing> GetByMount(int month)
        {
            using (var db = new OcphDbContext())
            {
                var result = (from m in db.Outgoing.Where(O => O.CreatedDate.Month == month)
                              join a in db.Agents.Select() on m.AgentId equals a.Id
                              join o in db.Ports.Select() on m.Origin equals o.Id
                              join d in db.Ports.Select() on m.Destination equals d.Id
                              select new ModelsShared.Models.manifestoutgoing
                              {
                                  AgentId = m.AgentId,
                                  Agent = a,
                                  Code = m.Code,
                                  CreatedDate = m.CreatedDate,
                                  DestinationPort = d,
                                  Destination = m.Destination,
                                  Id = m.Id,
                                  OnDestinationPort = m.OnDestinationPort,
                                  OnOriginPort = m.OnOriginPort,
                                  Origin = m.Origin,
                                  OriginPort = o,
                                  PortType = m.PortType,
                                  UpdateDate = m.UpdateDate,
                                  UserId = m.UserId
                              });

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        item.Information = db.ManifestInformations.Where(O => O.ManifestId == item.Id).FirstOrDefault();
                        item.PackingList = db.PackingLists.Where(O => O.ManifestID == item.Id).ToList();
                    }
                }
                return result;
            }
        }

        public bool UpdateInformation(ManifestInformation obj)
        {
            using (var db = new OcphDbContext())
            {
                if(obj.Id==0)
                {
                    var info =db.ManifestInformations.Where(O => O.ManifestId == obj.ManifestId).FirstOrDefault();
                    if (info == null)
                        return db.ManifestInformations.Insert(obj);
                    else
                        obj.Id = info.Id;
                }

               var res= db.ManifestInformations.Update(O => new { O.Address, O.ArmadaName, O.Contact, O.CrewName, O.PortType, O.ReferenceNumber },
                          obj, O => O.Id == obj.Id);
                return res;
            }
        }

        public manifestoutgoing Get(int Id)
        {
            using (var db = new OcphDbContext())
            {
                var result = (from m in db.Outgoing.Where(O => O.Id == Id)
                              join a in db.Agents.Select() on m.AgentId equals a.Id
                              join o in db.Ports.Select() on m.Origin equals o.Id
                              join d in db.Ports.Select() on m.Destination equals d.Id
                              select new ModelsShared.Models.manifestoutgoing
                              {
                                  AgentId = m.AgentId,
                                  Agent = a,
                                  Code = m.Code,
                                  CreatedDate = m.CreatedDate,
                                  DestinationPort = d,
                                  Destination = m.Destination,
                                  Id = m.Id,
                                  OnDestinationPort = m.OnDestinationPort,
                                  OnOriginPort = m.OnOriginPort,
                                  Origin = m.Origin,
                                  OriginPort = o,
                                  PortType = m.PortType,
                                  UpdateDate = m.UpdateDate,
                                  UserId = m.UserId
                              }).FirstOrDefault();

                if (result != null)
                {
                    result.Information = db.ManifestInformations.Where(O => O.ManifestId == result.Id).FirstOrDefault();
                    var plist = from a in db.PackingLists.Where(O => O.ManifestID == result.Id).ToList()
                                join b in db.Penjualans.Select() on a.PenjualanId equals b.Id
                                join c in db.Collies.Select() on a.PenjualanId equals c.PenjualanId
                                where c.CollyNumber.Equals(a.CollyNumber)
                                select new packinglist { CollyNumber = c.CollyNumber, Weight = c.Weight, Id = a.Id, STT = b.STT, ManifestID = a.ManifestID, PackNumber = a.PackNumber, PenjualanId = a.PenjualanId };
                    result.PackingList = plist.ToList();
                }
                return result;

            }
        }


     
        public manifestoutgoing InsertAndGetItem(manifestoutgoing t)
        {
            using (var db = new OcphDbContext())
            {
                var transaction = db.BeginTransaction();
                try
                {
                    t.Code = Helpers.GenerateOutgoingCode();
                    var date = DateTime.Now;
                    t.CreatedDate = date;
                    t.UpdateDate = date;
                    var manifestId = db.Outgoing.InsertAndGetLastID(t);
                    if (manifestId > 0)
                    {
                        foreach (var item in t.PackingList)
                        {
                            item.ManifestID = manifestId;
                            item.Id = db.PackingLists.InsertAndGetLastID(item);
                            if (item.Id <= 0)
                            {
                                throw new SystemException("Error");
                            }

                        }
                        transaction.Commit();
                        return t;

                    }
                    throw new SystemException("Error");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new SystemException(ex.Message);
                }
            }
        }

        public ManifestInformation InsertInformation(ManifestInformation obj)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    if(obj.Id<=0)
                    {
                        obj.Id = db.ManifestInformations.InsertAndGetLastID(obj);
                        if (obj.Id > 0)
                        {
                            return obj;
                        }
                        else
                        {
                            throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                        }
                    }else
                    {
                        if(db.ManifestInformations.Update(O => new { O.Address, O.ArmadaName, O.Contact, O.CrewName, O.ReferenceNumber },
                            obj, O => O.Id == obj.Id))
                        {
                            return obj;
                        }else
                        {
                            throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message);

                }
            }
        }

        public Task<List<PackingListPrintModel>> GetPackingList(int ManifestId)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var sp = string.Format("PackingList");
                    var cmd = db.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("ManifestId", ManifestId));
                    var dr = cmd.ExecuteReader();

                    var ent = new EntityInfo(typeof(ModelsShared.Models.PackingListPrintModel));
                    var list = new MappingColumn(ent).MappingWithoutInclud<PackingListPrintModel>(dr);
                    dr.Close();
                    
                    return Task.FromResult(list);
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }

            }
        }

        public Task<titipankapal> GetTitipanKapal(int ManifestId)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var sp = string.Format("TitipanKapal");
                    var cmd = db.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("ManifestId", ManifestId));
                    var dr = cmd.ExecuteReader();

                    var ent = new EntityInfo(typeof(ModelsShared.Models.titipankapal));
                    var list = new MappingColumn(ent).MappingWithoutInclud<titipankapal>(dr);
                    dr.Close();
                    titipankapal titip = null;
                    if (list.Count > 0)
                    {
                        titip = list.FirstOrDefault();
                        titip.Jumlah = list.Count;
                    }
                    return Task.FromResult(titip);
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
              
            }
        }

        public IEnumerable<manifestoutgoing> ManifestsByPenjualanId(int penjualanId)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var sp = string.Format("GetManifestsByPenjualan");
                    var cmd = db.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("penjualanId", penjualanId));
                    var dr = cmd.ExecuteReader();

                    var ent = new EntityInfo(typeof(ModelsShared.Models.manifestoutgoing));
                    var list = new MappingColumn(ent).MappingWithoutInclud<manifestoutgoing>(dr);
                    dr.Close();
                    return list;
                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }


        public manifestoutgoing UpdateOrigin(manifestoutgoing manifest)
        {
            using (var db = new OcphDbContext())
            {
                var isUpdated = db.Outgoing.Update(O => new { O.OnOriginPort }, manifest, O => O.Id == manifest.Id);
                if (isUpdated)
                    return manifest;
                else
                    throw new SystemException("Data Tidak tersimpan");
            }
        }

        public manifestoutgoing UpdateDestination(manifestoutgoing manifest)
        {
            using (var db = new OcphDbContext())
            {
                var isUpdated = db.Outgoing.Update(O => new { O.OnDestinationPort }, manifest, O => O.Id == manifest.Id);
                if (isUpdated)
                    return manifest;
                else
                    throw new SystemException("Data Tidak tersimpan");
            }
        }


    }
}
