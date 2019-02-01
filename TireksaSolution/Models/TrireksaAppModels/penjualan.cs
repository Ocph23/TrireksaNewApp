using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocph.DAL;

namespace ModelsShared.Models 
{
    [TableName("penjualan")]
    public class penjualan:BaseNotifyProperty
    {
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

        [DbColumn("STT")]
        public int STT
        {
            get { return _stt; }
            set
            {
                _stt = value;

                OnPropertyChange("STT");
            }
        }


        [DbColumn("ShiperID")]
        public int ShiperID
        {
            get { return _shiperid; }
            set
            {
                _shiperid = value;
                OnPropertyChange("ShiperID");
            }
        }

        [DbColumn("ReciverID")]
        public int ReciverID
        {
            get { return _reciverid; }
            set
            {
                _reciverid = value;
                OnPropertyChange("ReciverID");
            }
        }

        [DbColumn("Price")]
        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;
                SetTotal();
                OnPropertyChange("Price");
            }
        }

        [DbColumn("ChangeDate")]
        public DateTime ChangeDate
        {
            get { return _changedate; }
            set
            {
                _changedate = value;
                OnPropertyChange("ChangeDate");
            }
        }

        [DbColumn("UpdateDate")]
        public DateTime UpdateDate
        {
            get { return _updatedate; }
            set
            {
                _updatedate = value;
                OnPropertyChange("UpdateDate");
            }
        }

        [DbColumn("UserID")]
        public int UserID
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChange("UserID");
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [DbColumn("TypeOfWeight")]
        public TypeOfWeight TypeOfWeight
        {
            get { return _typeofweight; }
            set
            {
                _typeofweight = value;
                OnPropertyChange("TypeOfWeight");
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [DbColumn("PayType")]
        public virtual PayType PayType
        {
            get { return _paytype; }
            set
            {
                _paytype = value;
                OnPropertyChange("PayType");
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        [DbColumn("PortType")]
        public virtual PortType PortType
        {
            get { return _portType; }
            set
            {
                _portType = value;
                OnPropertyChange("PortType");
            }
        }
        [DbColumn("CityID")]
        public int CityID
        {
            get { return _cityid; }
            set
            {
                _cityid = value;
                OnPropertyChange("CityID");
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual CustomerIsPay CustomerIsPay
        {
            get { return _customerispay; }
            set
            {
                _customerispay = value;
                OnPropertyChange("CustomerIsPay");
            }
        }




        [DbColumn("CustomerIdIsPay")]
        public int CustomerIdIsPay
        {
            get
            {
                return _customerIdIsPay;
            }

            set { _customerIdIsPay = value;
              
                if (value == this.ReciverID)
                    CustomerIsPay = CustomerIsPay.Reciver;
                if (value == this.ShiperID)
                    CustomerIsPay = CustomerIsPay.Shiper;
                if (value != this.ShiperID && value != this.ReciverID)
                    CustomerIsPay = CustomerIsPay.Other;
                OnPropertyChange("CustomerIdIsPay");
            }
        }



        [DbColumn("PackingCosts")]
        public double PackingCosts
        {
            get { return _packingcosts; }
            set
            {
                    _packingcosts = value;
                SetTotal();
                OnPropertyChange("PackingCosts");
            }
        }

        [DbColumn("Tax")]
        public double Tax
        {
            get { return _tax; }
            set
            {
                _tax = value;
                SetTotal();
                OnPropertyChange("Tax");
            }
        }

        [DbColumn("Etc")]
        public double Etc
        {
            get { return _etc; }
            set
            {
                _etc = value;
                SetTotal();
                OnPropertyChange("Etc");
            }
        }


        private bool _actived;

        [DbColumn("Actived")]
        public bool Actived
        {
            get { return _actived; }
            set { _actived = value;
                OnPropertyChange("Actived");

            }
        }

        [DbColumn("IsPaid")]
        public bool IsPaid
        {
            get { return _isPaid; }
            set
            {
                _isPaid = value;
                OnPropertyChange("IsPaid");
            }
        }

        
        private string _content;
        [DbColumn("Content")]
        public string Content
        {
            get { return _content; }
            set { _content = value;
                OnPropertyChange("Content");
            }
        }


        private string _do;
        [DbColumn("DoNumber")]
        public string DoNumber
        {
            get { return _do; }
            set { _do = value;OnPropertyChange("DoNumber"); }
        }

        private string _note;

        [DbColumn("Note")]
        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChange("Note"); }
        }



        public List<colly> Details { get; set; }

        public double Total {
            get {

                if (Details != null && Details.Count > 0)
                {
                    double berat = 0;
                    berat = Details.Sum(O => O.Weight);
                    var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                    var tax = biaya * (this.Tax / 100);
                    _total = biaya + tax;
                }
                return _total; }
            set
            {
               
                _total = value;
                OnPropertyChange("Total");
            }


        }

        public void SetTotal()
        {
            if (Details != null && Details.Count > 0)
            {

                double berat = 0;
                berat = Details.Sum(O => O.Weight);
                Weight = berat;
                Pcs = Details.Count;
                var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                var tax = biaya * (this.Tax/100);
               Total = biaya + tax;
            }
        }


        public customer Reciver {
            get { return _reciever; }
            set { _reciever = value;OnPropertyChange("Reciver"); }
        }
        public customer Shiper {
            get { return _shiper; }
            set { _shiper = value; OnPropertyChange("Shiper"); }
        }

        public deliverystatus DeliveryStatus { get; set; }

        private int _pcs;

        public int Pcs
        {
            get
            {

                if (this.Details != null && this.Details.Count > 0)
                {
                    _pcs = Details.Count;
                }

                return _pcs;
            }
            set
            {
                _pcs = value; OnPropertyChange("Pcs");
            }
        }


        private double _weight;

        public double Weight
        {
            get
            {

                if (this.Details != null && this.Details.Count > 0)
                {
                    _weight = Details.Sum<ModelsShared.Models.colly>(O => O.Weight);
                }

                return _weight;
            }
            set { _weight = value; OnPropertyChange("Weight"); }
        }


        private int _stt;
        private int _shiperid;
        private int _reciverid;
        private double _price;
        private DateTime _changedate;
        private DateTime _updatedate;
        private int _userid;
        private TypeOfWeight _typeofweight;
        private PayType _paytype;
        private CustomerIsPay _customerispay;
        private double _packingcosts;
        private double _tax;
        private double _etc;
        private double _total;
        private PortType _portType;
        private int _cityid;
        private bool _isPaid;
        private int _customerIdIsPay;
        private int _id;
        private customer _reciever;
        private customer _shiper;
    }

}


