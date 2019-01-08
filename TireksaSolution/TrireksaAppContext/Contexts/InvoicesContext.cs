using ModelsShared;
using ModelsShared.Models;
using MySql.Data.MySqlClient;
using Ocph.DAL.Mapping;
using Ocph.DAL.Mapping.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrireksaAppContext
{
    public class InvoicesContext
    {
        // GET: api/Invoices
        public bool Delete(int id)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    var IsDeletedDetails = db.InvoiceDetails.Delete(O => O.InvoiceId == id);
                    if (IsDeletedDetails)
                    {
                        var IsDelete = db.Invoices.Delete(O => O.Id == id);
                        if (IsDelete)
                            trans.Commit();
                    }

                    throw new SystemException(MessageCollection.Message(MessageType.DeleteFail));

                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }


        public IEnumerable<invoice> Get(DateTime start, DateTime end)
        {
            using (var db = new OcphDbContext())
            {
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "GetAllInvoice";
                cmd.Parameters.Add(new MySqlParameter("StartDate", start));
                cmd.Parameters.Add(new MySqlParameter("EndDate", end));
    
                var reader = cmd.ExecuteReader();
                var result = MappingProperties<invoice>.MappingTable(reader);
                return result;
            }
        }


        public invoice Get(int Id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var inv = db.Invoices.Where(O => O.Id == Id).FirstOrDefault();
                   

                    var cmd = db.CreateCommand();
                    cmd.CommandText = "InvoiceDetail";
                    cmd.Parameters.Add(new MySqlParameter("invoiceId", Id));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();

                    var result = MappingProperties<invoicedetail>.MappingTable(reader);

                    reader.Close();

                    if (inv != null)
                        inv.Details = result;




                    return inv;
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }



        public invoice InsertAndGetItem(invoice t)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    var lastnumber = db.Invoices.GetLastItem();
                    if (lastnumber == null)
                    {
                        t.Number = 1;
                    }
                    else
                    {
                        t.Number = lastnumber.Number + 1;
                    }
                    t.Id = db.Invoices.InsertAndGetLastID(t);
                    if (t.Id <= 0)
                        throw new SystemException(MessageCollection.Message(MessageType.SaveFail));

                    foreach (var item in t.Details)
                    {
                        item.InvoiceId = t.Id;
                        item.Id = db.InvoiceDetails.InsertAndGetLastID(item);
                        if (item.Id <= 0)
                            throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                    }
                    trans.Commit();
                    return t;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new SystemException(ex.Message);


                }
            }
        }


        public invoice UpdateDeliveryDataAction(int Id, invoice t)
        {
            using (var db = new OcphDbContext())
            {
                var res= db.Invoices.Update(O => new { O.DeliveryDate, O.ReciveDate, O.ReciverBy, O.IsDelivery }, t, O => O.Id == Id);
                if (res)
                    return t;
                else
                    throw new SystemException("Data Tidak Tersimpan");
            }
        }


        public invoice UpdateInvoiceStatusAction(int Id, invoice t)
        {
            using (var db = new OcphDbContext())
            {
                var result = db.Invoices.Update(O => new { O.InvoicePayType, O.InvoiceStatus, O.PaidDate }, t, O => O.Id == Id);
                if (result)
                    return t;
                else
                    throw new SystemException("Data Tidak Tersimpan");
            }
        }


        public Task<invoice> GetInvoiceForPenjualanInfo(int Id)
        {
            invoice inv;
            using (var db = new OcphDbContext())
            {
             
                var det = db.InvoiceDetails.Where(O => O.PenjualanId == Id).FirstOrDefault();
                if (det != null)
                {
                    inv = db.Invoices.Where(O => O.Id == det.InvoiceId).FirstOrDefault();
                    inv.Details.Add(det);
                   return Task.FromResult(inv);
                }
                else
                {
                    inv = null;
                    return Task.FromResult(inv);
                }
                  
            }
        }

    }
}
