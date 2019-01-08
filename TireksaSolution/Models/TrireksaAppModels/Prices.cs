using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
    [TableName("prices")]
    public class Prices:BaseNotifyProperty
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

        [DbColumn("ShiperId")]
        public int ShiperId
        {
            get { return _customerid; }
            set
            {
                _customerid = value;
                OnPropertyChange("CustomerId");
            }
        }
        [DbColumn("ReciverId")]
        public int ReciverId
        {
            get { return _relation; }
            set
            {
                _relation = value;
                OnPropertyChange("Relation");
            }
        }
        private PortType _portType;
        [DbColumn("PortType")]

        public PortType PortType
        {
            get { return _portType; }
            set { _portType = value;
                OnPropertyChange("PortType"); }
        }


        private PayType _payType;
        [DbColumn("PayType")]

        public PayType PayType
        {
            get { return _payType; }
            set { _payType = value;
                OnPropertyChange("PayType");
            }
        }



        [DbColumn("FromCity")]
        public int From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChange("From");
            }
        }

        [DbColumn("ToCity")]
        public int To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChange("To");
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

        private int _id;
        private int _customerid;
        private int _from;
        private int _to;
        private int _relation;
        private double _price;

      
    }

}
