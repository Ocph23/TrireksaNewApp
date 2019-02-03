using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;

namespace TrireksaApp.Reports.Models
{
    public class InvoiceReportModel : ModelsShared.Models.invoicedetail
    {
        public DateTime CreateDate { get; internal set; }
        public string CustomerName { get; internal set; }
        public DateTime DeadLine { get; internal set; }
        public string NumberView { get; internal set; }
        public string Terbilang { get; internal set; }
    }

    public class InvoiceReport : ModelsShared.Models.invoice
    {
  

        public InvoiceReport(invoice item)
        {
            this.InvoiceStatus = item.InvoiceStatus;
            this.InvoicePayType = item.InvoicePayType;
            this.Biaya = item.Biaya;
            this.CreateDate = item.CreateDate;
            this.CustomerName = item.CustomerName;
            this.Tax = item.Tax;
            this.Biaya = item.Biaya;
            Total = item.Total;
            PaidDate = item.PaidDate;
            DeadLine = item.DeadLine;
        }

        public string Status
        {
            get { return this.InvoiceStatus.ToString(); }
        }

        public string PaymentType { get { return InvoicePayType.ToString(); } }

    }
}
