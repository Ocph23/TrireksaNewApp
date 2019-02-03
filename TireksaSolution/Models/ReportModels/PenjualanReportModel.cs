using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.ReportModels
{
    public class PenjualanReportModel
    {
        private double _biaya;
        public int Id { get; set; }
        public string STT { get; set; }
        public string Shiper { get; set; }
        public string Reciver { get; set; }
        public string ShiperCity { get; set; }
        public string ReciverCity { get; set; }
        public string Pcs { get; set; }
        public string PayType { get; set; }
        public string WeightType { get; set; }
        public string PortType { get; set; }
        public double Long { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Weight { get; set; }
        public string PayStatus { get; set; }
        public bool SendStatus { get; set; }
        public double Price { get; set; }
        public DateTime ChangeDate { get; set; }
        public string DoNumber { get; set; }
        public virtual double Biaya{ get { return Price * Weight; } set { _biaya = value; } }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
