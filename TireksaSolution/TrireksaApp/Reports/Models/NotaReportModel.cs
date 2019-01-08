using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TrireksaApp.Reports.Models
{
    public class NotaReportModel
    {
        public string OriginPortCode { get; set; }
        public string DestinationPortCode { get; set; }
        public int Pcs { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string PortTypeName { get; set; }
        public string PayTypeName { get; set; }
        public string DoNumber { get; set; }
        public double Costs { get; set; }
        public double TaxValue { get; set; }
        public double GradTotal { get; set; }
        public string UserName { get; set; }
        public string Note { get; set; }
        public string Content { get; internal set; }
        public double Price { get; internal set; }
        public double Tax { get; internal set; }
        public TypeOfWeight TypeOfWeight { get; private set; }
        public double PackingCosts { get; internal set; }
        public double Etc { get; internal set; }
        public string Reciver { get; internal set; }
        public string Shiper { get; internal set; }
        public string STT { get; internal set; }
        public string ShiperCity { get; internal set; }
        public string ReciverCity{ get; internal set; }
        public byte[] STTQRCode { get; internal set; }
        public DateTime ChangeDate { get; internal set; }
    }
}
