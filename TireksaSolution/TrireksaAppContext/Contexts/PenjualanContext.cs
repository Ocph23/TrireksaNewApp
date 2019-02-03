using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Ocph.DAL;
using Ocph.DAL.Mapping;
using System.Threading.Tasks;
using ModelsShared.ReportModels;
using ModelsShared;
using Ocph.DAL.Mapping.MySql;

namespace TrireksaAppContext
{
   public class PenjualanContext
    {
        public IEnumerable<penjualan> Get(DateTime start, DateTime end)
        {

            using (var db = new OcphDbContext())
            {
                var sp = string.Format("GetPenjualans");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("StartDate", start));
                cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("EndDate", end));
                var dr = cmd.ExecuteReader();
                var list = MappingProperties<penjualan>.MappingTable(dr);
                dr.Close();
                foreach (var item in list)
                {
                    item.Details = db.Collies.Where(O => O.PenjualanId == item.Id).ToList();
                    item.Shiper = db.Customers.Where(O => O.Id == item.ShiperID).FirstOrDefault();
                    if (item.Shiper != null)
                        item.Shiper.City = db.Cities.Where(O => O.Id == item.Shiper.CityID).FirstOrDefault();
                    item.Reciver = db.Customers.Where(O => O.Id == item.ReciverID).FirstOrDefault();
                    if (item.Reciver != null)
                        item.Reciver.City = db.Cities.Where(O => O.Id == item.Reciver.CityID).FirstOrDefault();

                    item.DeliveryStatus = db.DeliveryStatusses.Where(O => O.PenjualanId == item.Id).FirstOrDefault();
                }
                return list;
            }
        }

        // POST: api/Penjualans
        public int NewSTT()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Penjualans.Select(O => new {O.STT }).Last();
                    if (result != null)
                        return result.STT + 1;
                    else
                        return 1;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
               
            }
        }

       
        public penjualan InsertAndGetItem(penjualan value)
        {
            var date = DateTime.Now;
            value.ChangeDate = date;
            value.UpdateDate = date;
            using (var db = new OcphDbContext())
            {
                if (value.Details != null && value.Details.Count > 0)
                {
                    var tr = db.BeginTransaction();
                    try
                    {
                        if (value.STT == 0)
                            value.STT = NewSTT();

                        value.Id = db.Penjualans.InsertAndGetLastID(value);

                        if (value.Id > 0)
                        {

                            foreach (var item in value.Details)
                            {
                                item.PenjualanId = value.Id;
                                item.Id = db.Collies.InsertAndGetLastID(item);
                            }

                            tr.Commit();
                        }
                        else
                        {
                            throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                        }

                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        throw new SystemException(ex.Message);
                    }
                }
                else
                    throw new SystemException("Item Berang Belum ada");
            }
            return value;

        }

        public penjualan Update(int id, penjualan value)
        {
            var date = DateTime.Now;
            value.UpdateDate = date;
            using (var db = new OcphDbContext())
            {
                if (value.Details != null && value.Details.Count > 0)
                {
                    var tr = db.BeginTransaction();
                    try
                    {
                        var dbcolies = db.Collies.Where(O => O.PenjualanId == value.Id).ToList();
                        foreach (var item in value.Details)
                        {
                            if (item.Id > 0)
                            {
                                var saved = db.Collies.Update(O => new { O.CollyNumber, O.Hight, O.Long, O.Weight, O.Wide, O.TypeOfWeight },
                               item, O => O.Id == item.Id);
                                if (!saved)
                                    throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
                            }
                            else
                            {
                                item.Id = db.Collies.InsertAndGetLastID(item);
                                if (item.Id <= 0)
                                    throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
                            }
                        }

                        foreach (var item in dbcolies)
                        {
                            var ondata = value.Details.Find(O => O.Id == item.Id);
                            if (ondata == null)
                            {
                                db.Collies.Delete(O => O.Id == item.Id);
                            }
                        }

                        var savedParent = db.Penjualans.Update(O => new
                        {
                            O.UpdateDate,
                            O.From, O.To,
                            O.Content,
                            O.CustomerIdIsPay,
                            O.DoNumber,
                            O.Etc,
                            O.IsPaid,
                            O.Note,
                            O.PackingCosts,
                            O.PayType,
                            O.PortType,
                            O.Price,
                            O.ReciverID,
                            O.ShiperID,
                            O.STT,
                            O.Tax,
                            O.TypeOfWeight,
                            O.UserID,
                            O.ChangeDate
                        }, value, O => O.Id == value.Id);
                        if (!savedParent)
                            throw new SystemException(MessageCollection.Message(MessageType.UpdateFaild));
                        tr.Commit();

                        return value;
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        throw new SystemException(ex.Message);
                    }
                }
                else
                    throw new SystemException("Item Berang Belum ada");
            }

        }

        public deliverystatus UpdateDeliveryStatusBySTT(int id, deliverystatus obj)
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var res = db.Penjualans.Where(O => O.STT == id).FirstOrDefault();
                    if (res != null)
                    {
                        obj.PenjualanId = res.Id;
                        
                        var deliveryId = db.DeliveryStatusses.Where(O => O.PenjualanId == res.Id).FirstOrDefault().Id;
                        if (deliveryId <= 0)
                            throw new SystemException("Delivery Status Tidak Ditemukan");
                        else
                            obj.Id = deliveryId;

                        var isUpdated = db.DeliveryStatusses.Update(O => new { O.Description, O.IsSignIn, O.Phone, O.ReciveDate, O.ReciveName, O.UserID }, obj, O => O.Id == obj.Id && O.PenjualanId == obj.PenjualanId);
                        if (isUpdated)
                            return obj;
                       else
                            throw new SystemException("Tidak Tersimpan");
                    }
                    else
                        throw new SystemException("STT Tidak Ditemukan");

                }
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        private bool CheckPortAccess(penjualan value)
        {
            using (var db = new OcphDbContext())
            {
                var ports = db.Ports.Where(O => O.CityID == value.To && O.PortType == value.PortType).FirstOrDefault();
                if (ports != null)
                    return true;
                else
                    return false;
            }
        }

        public IEnumerable<penjualan> GetByParameter(int agentId, PortType type)
        {
            var list = new List<penjualan>();
            using (var db = new OcphDbContext())
            {
                var cities = from c in db.CitiesAgentCanAccess.Where(O => O.AgentId == agentId)
                             join p in db.Penjualans.Where(O => O.PortType == type) on c.CityId equals p.To
                             select p;

                foreach (var item in cities.ToList())
                {
                    item.Details = db.Collies.Where(O => O.PenjualanId == item.Id && O.IsSended == false).ToList();
                    if (item.Details != null && item.Details.Count > 0)
                    {
                        item.Shiper = db.Customers.Where(O => O.Id == item.ShiperID).FirstOrDefault();
                        item.Reciver = db.Customers.Where(O => O.Id == item.ReciverID).FirstOrDefault();
                        list.Add(item);
                    }
                }
                return list;
            }


        }

        public penjualan Get(int STT)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Penjualans.Where(O => O.STT == STT).FirstOrDefault();
                if (result != null)
                    result.Details = db.Collies.Where(O => O.PenjualanId== result.Id).ToList();

                return result;
            }
        }


        public penjualan GetById(int penjualanId)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Penjualans.Where(O => O.Id == penjualanId).FirstOrDefault();
                if (result != null)
                    result.Details = db.Collies.Where(O => O.PenjualanId == result.Id).ToList();

                return result;
            }
        }


        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<penjualan> GetPenjualanNotPaid(int Id)
        {
            using (var db = new OcphDbContext())
            {
                PayType status = PayType.Credit;
               
                var result = db.Penjualans.Where(O => O.PayType ==  status && O.CustomerIdIsPay == Id && O.IsPaid == false);
                foreach (var item in result)
                {
                    item.Shiper = db.Customers.Where(o => o.Id == item.ShiperID).FirstOrDefault();
                    item.Reciver = db.Customers.Where(o => o.Id == item.ReciverID).FirstOrDefault();
                    item.Details = db.Collies.Where(o => o.PenjualanId == item.Id).ToList();
                    item.DeliveryStatus = db.DeliveryStatusses.Where(O => O.PenjualanId == item.Id).FirstOrDefault();
                }

                return result;
            }
        }

        public deliverystatus UpdateDeliveryStatus(deliverystatus obj)
        {
            using (var db = new OcphDbContext())
            {
                var isUpdated = db.DeliveryStatusses.Update(O => new { O.Description, O.IsSignIn, O.Phone, O.ReciveDate, O.ReciveName, O.UserID },
                    obj, O => O.Id == obj.Id && O.PenjualanId==obj.PenjualanId);
                if (isUpdated)
                {
                    return obj;
                }else
                {
                    throw new SystemException("Tidak Tersimpan");
                }
            }
        }


        public bool IsSended(int Id)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Collies.Where(O => O.PenjualanId == Id && O.IsSended == true);
                if (result.Count() > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }



        public  List<PenjualanReportModel> GetPenjualanFromTo(DateTime startDate, DateTime ended)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var sp = string.Format("GetPenjualanFromTo");
                    var cmd = db.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = sp;
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("startDate", startDate));
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("ended", ended));
                    var dr = cmd.ExecuteReader();

                    var list = MappingProperties<PenjualanReportModel>.MappingTable(dr);
                    dr.Close();
                    foreach(var item in list)
                    {
                        item.From = startDate;
                        item.To = ended;
                    }
                    return list;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }

            }
        }



    }
}

