using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
