using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared.Models
{
    [TableName("deliverystatus")]
    public class deliverystatus:BaseNotifyProperty
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

        [DbColumn("PenjualanId")]
        public int PenjualanId
        {
            get { return _penjualanId; }
            set
            {
                 _penjualanId= value;
                OnPropertyChange("PenjualanId");
            }
        }

        [DbColumn("ReciveDate")]
        public DateTime ReciveDate
        {
            get { return _recivedate; }
            set
            {
                _recivedate = value;
                OnPropertyChange("ReciveDate");
            }
        }

        [DbColumn("ReciveName")]
        public string ReciveName
        {
            get { return _recivename; }
            set
            {
                _recivename = value;
                OnPropertyChange("ReciveName");
            }
        }

        [DbColumn("Phone")]
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChange("Phone");
            }
        }

        [DbColumn("IsSignIn")]
        public bool IsSignIn
        {
            get { return _issignin; }
            set
            {
                _issignin = value;
                OnPropertyChange("IsSignIn");
            }
        }

        [DbColumn("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChange("Description");
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

        private int _id;
        private DateTime _recivedate;
        private string _recivename;
        private string _phone;
        private bool _issignin;
        private string _description;
        private string _userid;
        private int _penjualanId;
    }

}
