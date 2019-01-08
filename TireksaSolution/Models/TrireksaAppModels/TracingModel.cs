using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelsShared.Models;
using Ocph.DAL;

namespace ModelsShared.Models
{
    public class TracingModel : BaseNotifyProperty
    {
        public List<manifestoutgoing> Manifests { get; set; }

        private string _shiperName;

        [DbColumn("ShiperName")]
        public string ShiperName
        {
            get { return _shiperName; }
            set { _shiperName = value; }
        }

        private string _reciverName;
        [DbColumn("ReciverName")]

        public string ReciverName
        {
            get { return _reciverName; }
            set { _reciverName = value; }
        }

        private string _reciveName;
        [DbColumn("ReciveName")]

        public string ReciveName
        {
            get { return _reciveName; }
            set { _reciveName = value; }
        }


        private DateTime _reciveDate;
        [DbColumn("ReciveDate")]
        public DateTime ReciveDate
        {
            get { return _reciveDate; }
            set { _reciveDate = value; }
        }

        private string _phone;

        [DbColumn("Phone")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }


        private string _PortOriginName;
        [DbColumn("PortOriginName")]
        public string PortOriginName
        {
            get { return _PortOriginName; }
            set { _PortOriginName = value; }
        }

        private string _PortDestinationName;
        [DbColumn("PortDestinationName")]
        public string PortDestinationName
        {
            get { return _PortDestinationName; }
            set { _PortDestinationName = value; }
        }
        private string _PortDestionationCode;
        [DbColumn("PortDestionationCode")]
        public string PortDestionationCode
        {
            get { return _PortDestionationCode; }
            set { _PortDestionationCode = value; }
        }


        private string _PortOriginCode;
        [DbColumn("PortOriginCode")]
        public string PortOriginCode
        {
            get { return _PortOriginCode; }
            set { _PortOriginCode = value; }
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
                OnPropertyChange("Price");
            }
        }

        [DbColumn("ChangeDate")]
        public DateTime? ChangeDate
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
        public string UserID
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChange("UserID");
            }
        }

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

        //   [DbColumn("CustomerIsPay")]
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
                if (CustomerIsPay == CustomerIsPay.Reciver)
                    return this.ReciverID;
                else
                    return this.ShiperID;
            }
            set
            {
                _customerIdIsPay = value;
                if (value == this.ReciverID)
                    CustomerIsPay = CustomerIsPay.Reciver;
                if (value == this.ShiperID)
                    CustomerIsPay = CustomerIsPay.Shiper;

            }
        }



        [DbColumn("PackingCosts")]
        public double PackingCosts
        {
            get { return _packingcosts; }
            set
            {
                _packingcosts = value;
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
                OnPropertyChange("Etc");
            }
        }


        private bool _actived;

        [DbColumn("Actived")]
        public bool Actived
        {
            get { return _actived; }
            set
            {
                _actived = value;
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
            set
            {
                _content = value;
                OnPropertyChange("Content");
            }
        }


        private string _do;
        [DbColumn("DoNumber")]
        public string DoNumber
        {
            get { return _do; }
            set { _do = value; OnPropertyChange("DoNumber"); }
        }

        private string _note;

        [DbColumn("Note")]
        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChange("Note"); }
        }

        
        public double Total
        {
            get
            {

                if (Weight > 0 && Pcs> 0)
                {
                    double berat = 0;
                    berat = Weight;
                    var biaya = (berat * this.Price) + this.PackingCosts + this.Etc;
                    var tax = biaya * (this.Tax / 100);
                    _total = biaya + tax;
                }
                return _total;
            }
            set
            {

                _total = value;
                OnPropertyChange("Total");
            }


        }

  
     
        private int _pcs;
        [DbColumn("Pcs")]
        public int Pcs
        {
            get
            {

               

                return _pcs;
            }
            set
            {
                _pcs = value;
                OnPropertyChange("Pcs");
            }
        }


        private double _weight;
        [DbColumn("Weight")]
        public double Weight
        {
            get
            {

               

                return _weight;
            }
            set { _weight = value; OnPropertyChange("Weight"); }
        }

        [DbColumn("Id")]
        public int Id {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        private int _stt;
        private int _shiperid;
        private int _reciverid;
        private double _price;
        private DateTime? _changedate;
        private DateTime _updatedate;
        private string _userid;
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



    }
}