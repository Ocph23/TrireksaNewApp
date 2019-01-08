using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModelsShared.Models
{
    public delegate void SetTotal();

    [TableName("invoicedetail")]
    public class invoicedetail:BaseNotifyProperty
    {
        public event SetTotal TotalAction;

        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("PenjualanId")]
        public int PenjualanId
        {
            get { return _penjualanId; }
            set
            {
                _penjualanId = value;
                OnPropertyChange("PenjualanId");
            }
        }

        [DbColumn("InvoiceId")]
        public int InvoiceId
        {
            get { return _invId; }
            set
            {
                _invId = value;
                OnPropertyChange("InvoiceId");
            }
        }



        private string _reciver;

        public string Reciver
        {
            get { return _reciver; }
            set { _reciver = value;
                OnPropertyChange("Reciver");
            }
        }

        private string _shiper;

        public string Shiper
        {
            get { return _shiper; }
            set { _shiper = value;
                OnPropertyChange("Shiper");
            }
        }

        private int _pcs;

        public int Pcs
        {
            get { return _pcs; }
            set { _pcs = value; OnPropertyChange("Pcs"); }
        }

        private double _weight;

        public double Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChange("Weight"); }
        }

        private double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChange("Price"); }
        }
        private double _total;

        public double Total
        {
            get
            {
                    double berat = 0;
                    berat = this.Weight;
                    var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                    var tax = biaya * (this.Tax / 100);
                    _total = biaya + tax;
                
                return _total;
            }
            set
            {

                _total = value;
                OnPropertyChange("Total");
            }


        }

        private DateTime changeDate;

        public DateTime ChangeDate
        {
            get { return changeDate; }
            set { changeDate = value;
                OnPropertyChange("ChangeDate");
            }
        }



        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value;
                OnPropertyChange("IsSelected");

                if (TotalAction != null)
                    TotalAction.Invoke();
            }
        }


        public double PackingCosts { get; set; }
        public double Etc { get; set; }
        public double Tax { get; set; }
        public int STT { get; set; }
        public string DoNumber { get; set; }
        public string Tujuan { get;  set; }
        public PortType PortType { get; set; }
        public virtual string Via {
            get
            {
                switch (PortType)
                {
                    case PortType.Sea:
                        return "Laut";
                    case PortType.Air:
                        return "Udara";
                    case PortType.Land:
                        return "Darat";
                    default:
                        return string.Empty;
                }
            }
            set { _via = value; }
        }

        private int _id;
        private int _invId;
        private int _penjualanId;
        private string _via;
    }
}
