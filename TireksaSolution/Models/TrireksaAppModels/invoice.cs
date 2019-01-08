using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ModelsShared.Models
{
    [TableName("invoices")]
    public class invoice:BaseNotifyProperty
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

        [DbColumn("Number")]
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NumberView = ModelHelpers.GenerateInvoiceCode(Number, this.CreateDate);
                OnPropertyChange("Number");
            }
        }

        public string NumberView
        {
            get {
                return _numberView; }
            set
            {
                _numberView = value;
                OnPropertyChange("NumberView");
            }
        }


        [DbColumn("CustomerId")]
        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                _customerId = value;
                OnPropertyChange("CustomerId");
            }
        }

        [DbColumn("IsDelivery")]
        public bool IsDelivery
        {
            get { return _isdelivery; }
            set
            {
                _isdelivery = value;
                OnPropertyChange("IsDelivery");
            }
        }

        [DbColumn("InvoiceStatus")]
        public InvoiceStatus InvoiceStatus
        {
            get { return _invoicestatus; }
            set
            {
                _invoicestatus = value;
                OnPropertyChange("InvoiceStatus");
            }
        }

        [DbColumn("DeliveryDate")]
        public DateTime? DeliveryDate
        {
            get { return _deliverydate; }
            set
            {
                _deliverydate = value;
                OnPropertyChange("DeliveryDate");
            }
        }

        [DbColumn("ReciverBy")]
        public string ReciverBy
        {
            get { return _reciverby; }
            set
            {
                _reciverby = value;
                OnPropertyChange("ReciverBy");
            }
        }

        [DbColumn("ReciveDate")]
        public DateTime? ReciveDate
        {
            get { return _recivedate; }
            set
            {
                _recivedate = value;
                OnPropertyChange("ReciveDate");
            }
        }

        [DbColumn("DeadLine")]
        public DateTime DeadLine
        {
            get { return _tempodate; }
            set
            {
                _tempodate = value;
                OnPropertyChange("DeadLine");
            }
        }

        [DbColumn("InvoicePayType")]
        public InvoicePayType InvoicePayType
        {
            get { return _invoicepaytype; }
            set
            {
                _invoicepaytype = value;
                OnPropertyChange("InvoicePayType");
            }
        }

        [DbColumn("CreateDate")]
        public DateTime CreateDate
        {
            get { return _createdate; }
            set
            {
                _createdate = value;
                NumberView = ModelHelpers.GenerateInvoiceCode(Number, value);
                OnPropertyChange("CreateDate");
            }
        }

        [DbColumn("UserId")]
        public int UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
                OnPropertyChange("UserId");
            }
        }

        public string  CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
                OnPropertyChange("CustomerName");
            }
        }

        public double Tax { get; set; }
        public double Biaya { get; set; }

        public virtual double Total
        {
            get { return Biaya + (Biaya * (Tax / 100)); }

            set { _total = value; }
        }

        public List<invoicedetail> Details
        {
            get
            {
                if(_details==null)
                {
                    _details = new List<invoicedetail>();
                }

                return _details;
            }

            set
            {
                _details = value;
                OnPropertyChange("Details");
            }
        }


        private DateTime? _paidDate;
        [DbColumn("PaidDate")]
        public DateTime? PaidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; OnPropertyChange("PaidDate"); }
        }



        private int _id;
        private int _number;
        private bool _isdelivery;
        private InvoiceStatus _invoicestatus;
        private DateTime? _deliverydate;
        private string _reciverby;
        private DateTime? _recivedate;
        private DateTime _tempodate;
        private InvoicePayType _invoicepaytype;
        private DateTime _createdate;
        private int _userid;
        private int _customerId;
        private string _customerName;
        private List<invoicedetail> _details;
        private string _numberView;
        private double _total;
    }
}

