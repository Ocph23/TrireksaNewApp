using ModelsShared;
using ModelsShared.Models;
using Ocph.DAL;
using Ocph.DAL.Mapping;
using Ocph.DAL.Mapping.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaAppContext
{
   public  class DashboardContext
    {
        public Task<List<invoice>> GetInvoiceNotYetPaid()
        {
            using (var db = new OcphDbContext())
            {

                var sp = string.Format("InvoiceNotYetPaid");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                var dr = cmd.ExecuteReader();

                var ent = new EntityInfo(typeof(ModelsShared.Models.invoice));
                var list = new MappingColumn(ent).MappingWithoutInclud<invoice>(dr);
                dr.Close();

                return Task.FromResult(list);
            }

        }
        public Task<double> GetPenjualanBulan(int month, int year)
        {

            using (var db = new OcphDbContext())
            {
                var result = db.Penjualans.Where(O => O.ChangeDate.Month == month && O.ChangeDate.Year == year);
                foreach (var item in result)
                {
                    item.Details = db.Collies.Where(O => O.PenjualanId == item.Id).ToList();
                    item.Shiper = db.Customers.Where(O => O.Id == item.ShiperID).FirstOrDefault();
                    item.Reciver = db.Customers.Where(O => O.Id == item.ReciverID).FirstOrDefault();
                    item.DeliveryStatus = db.DeliveryStatusses.Where(O => O.PenjualanId == item.Id).FirstOrDefault();
                }

                return Task.FromResult(result.Sum(O => O.Total));
            }
        }

        public Task<List<penjualan>> GetPenjualanNotHaveStatus()
        {
            using (var db = new OcphDbContext())
            {

                var sp = string.Format("PenjualanNotHaveDeliveryStatus");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                var dr = cmd.ExecuteReader();

                var ent = new EntityInfo(typeof(ModelsShared.Models.penjualan));
                var list = new MappingColumn(ent).MappingWithoutInclud<penjualan>(dr);
                dr.Close();
                foreach (var item in list)
                {
                    item.Shiper = db.Customers.Where(O => O.Id == item.ShiperID).FirstOrDefault();
                    item.Reciver = db.Customers.Where(O => O.Id == item.ReciverID).FirstOrDefault();
                }
                return Task.FromResult(list);
            }


        }


        public Task<List<penjualan>> GetPenjualanNotYetSend()
        {
            using (var db = new OcphDbContext())
            {
                var sp = string.Format("PenjualanNotYetSend");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                var dr = cmd.ExecuteReader();
                var ent = new EntityInfo(typeof(ModelsShared.Models.penjualan));
                var list = new MappingColumn(ent).MappingWithoutInclud<penjualan>(dr);

                dr.Close();

                foreach (var item in list)
                {
                    item.Shiper = db.Customers.Where(O => O.Id == item.ShiperID).FirstOrDefault();
                    item.Reciver = db.Customers.Where(O => O.Id == item.ReciverID).FirstOrDefault();
                }
                return Task.FromResult(list);
            }


        }


        public Task<List<penjualan>> GetPenjualanNotPaid()
        {

            using (var db = new OcphDbContext())
            {
                var list = db.Penjualans.Where(O => O.PayType == PayType.Credit && O.IsPaid == false).ToList();

                foreach (var item in list)
                {
                    item.Shiper = db.Customers.Where(O => O.Id == item.ShiperID).FirstOrDefault();
                    item.Reciver = db.Customers.Where(O => O.Id == item.ReciverID).FirstOrDefault();
                }
                return Task.FromResult(list);
            }
        }

        public Task<List<invoice>> GetInvoiceNotYetDelivery()
        {
            using (var db = new OcphDbContext())
            {

                var sp = string.Format("InvoiceNotYetDelivery");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                var dr = cmd.ExecuteReader();

                var ent = new EntityInfo(typeof(ModelsShared.Models.invoice));
                var list = new MappingColumn(ent).MappingWithoutInclud<invoice>(dr);
                dr.Close();

                return Task.FromResult(list);
            }

        }

        public Task<List<invoice>> GetInvoiceJatuhTempo()
        {
            using (var db = new OcphDbContext())
            {

                var sp = string.Format("InvoiceJatuhTempo");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("Tanggal", DateTime.Now));
                var dr = cmd.ExecuteReader();

                var ent = new EntityInfo(typeof(ModelsShared.Models.invoice));
                var list = new MappingColumn(ent).MappingWithoutInclud<invoice>(dr);
                dr.Close();

                return Task.FromResult(list);
            }

        }

        public Task<List<invoice>> GetInvoiceNotYetRecive()
        {
            using (var db = new OcphDbContext())
            {

                var sp = string.Format("InvoiceNotYetRecive");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                var dr = cmd.ExecuteReader();

                var ent = new EntityInfo(typeof(ModelsShared.Models.invoice));
                var list = new MappingColumn(ent).MappingWithoutInclud<invoice>(dr);
                dr.Close();

                return Task.FromResult(list);
            }

        }


        public async Task<List<PenjualanOfYear>> GetPenjualanThreeYear()
        {
            var list = new List<ModelsShared.Models.PenjualanOfYear>();
            using (var db = new OcphDbContext())
            {
                var thisyear = DateTime.Now.Year;
                var sp = string.Format("PenjualanOfaYear");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                for (int i = thisyear; i > thisyear - 3; i--)
                {
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("tahun", i));
                    var dr = cmd.ExecuteReader();

                    var ent = new EntityInfo(typeof(ModelsShared.Models.PenjualanOfYear));
                    list = new MappingColumn(ent).MappingWithoutInclud<PenjualanOfYear>(dr);
                    if (list.Count > 0)
                    {
                        var item = list.FirstOrDefault();
                        item.Months = await GetPenjualanOfMonth(item.Tahun);
                        list.Add(item);
                    }


                    cmd.Parameters.RemoveAt("tahun");
                    dr.Close();
                }
            }

            return list;
        }

        private Task<List<PenjualanOfMonth>> GetPenjualanOfMonth(int tahun)
        {
            var list = new List<ModelsShared.Models.PenjualanOfMonth>();
            using (var db = new OcphDbContext())
            {
                var sp = string.Format("PenjualanOfamount");
                var cmd = db.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = sp;
                for (int i = 1; i <= 12; i++)
                {
                    var date = new DateTime(tahun, i, 1);
                    cmd.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("Tanggal", date));
                    var dr = cmd.ExecuteReader();
                    var ent = new EntityInfo(typeof(ModelsShared.Models.PenjualanOfMonth));
                    var result = new MappingColumn(ent).MappingWithoutInclud<PenjualanOfMonth>(dr);
                    if (result.Count > 0)
                    {
                        var item = result.FirstOrDefault();
                        list.Add(item);
                    }


                    cmd.Parameters.RemoveAt("Tanggal");
                    dr.Close();
                }
            }

            return Task.FromResult(list);
        }


    }
}
