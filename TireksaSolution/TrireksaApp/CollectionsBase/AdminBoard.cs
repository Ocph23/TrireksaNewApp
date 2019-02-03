using ModelsShared.Models;
using ModelsShared.ReportModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class AdminBoard
    {
        private Client client = new Client("Dashboard");

        internal Task<double> GetPenjualanBulan(DateTime date)
        {
            var uri = "GetPenjualanBulan?month="+date.Month+"&year=" + date.Year;
            return client.GetAsync<double>(uri);
        }

        internal Task<List<PenjualanReportModel>> GetPenjualanNotPaid()
        {
            return client.GetAsync<List<PenjualanReportModel>>("GetPenjualanNotPaid");
        }

        internal Task<List<PenjualanReportModel>> GetPenjualanNotStatus()
        {
            return client.GetAsync<List<PenjualanReportModel>>("GetPenjualanNotHaveStatus");
        }

        internal  Task<List<invoice>> GetInvoiceNotYetPaid()
        {
            return client.GetAsync<List<invoice>>("GetInvoiceNotYetPaid");
        }

        internal Task<List<invoice>> GetInvoiceNotYetDelivery()
        {
            return client.GetAsync<List<invoice>>("GetInvoiceNotYetDelivery");
        }
        internal Task<List<invoice>> GetInvoiceNotYetRecive()
        {
            return client.GetAsync<List<invoice>>("GetInvoiceNotYetRecive");
        }

        internal Task<List<invoice>> GetInvoiceJatuhTempo()
        {
            return client.GetAsync<List<invoice>>("GetInvoiceJatuhTempo");
        }

        internal Task<List<PenjualanReportModel>> GetPenjualanNotYetSend()
        {
            return client.GetAsync<List<PenjualanReportModel>>("GetPenjualanNotYetSend");
        }
    }
}
