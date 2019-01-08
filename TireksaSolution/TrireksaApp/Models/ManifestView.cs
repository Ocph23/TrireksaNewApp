using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsShared.Models;

namespace TrireksaApp.Models
{
   public  class ManifestView:NotifyPropertyChanged
    {

        private string _shiperName;

        public string ShiperName
        {
            get { return _shiperName; }
            set { _shiperName = value; OnPropertyChanged("ShiperName"); }
        }

        private string _recivername;

        public string ReciverName
        {
            get { return _recivername; }
            set
            {
                _recivername = value;
                OnPropertyChanged("ReciverName");
            }
        }

        private int _pcs;

        public int Pcs
        {
            get
            {
                return _pcs;
            }
            set { _pcs = value; OnPropertyChanged("Pcs"); }
        }


        private double _weightView;

        public double WeightView
        {
            get
            {

               

                return _weightView;
            }
            set { _weightView = value; OnPropertyChanged("WeightView"); }
        }

        public string AgentName { get; internal set; }

        public string ManifestCode
        {
            get
            {
                if (this.Code > 0)
                    return Helper.GenerateManifestOutGoingCode(Code, this.CreatedDate);
                else
                    return string.Empty;
            }
        }

        public int Code { get; set; }
        public DateTime CreatedDate { get;  set; }
        public string STT { get; internal set; }
        public string PortType { get; internal set; }
        public PayType PayType { get; internal set; }
    }
}
